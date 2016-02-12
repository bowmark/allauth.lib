using System;
using System.Collections.Generic;
using System.Net;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using AllAuth.Lib.APIs;
using AllAuth.Lib.Crypto;
using Newtonsoft.Json;
using RestSharp;

namespace AllAuth.Lib.ServerAPI.Requests
{
    public abstract class RequestAbstract<TResponse>
    {
        private readonly Dictionary<string, object> _requestData = new Dictionary<string, object>();
        
        protected abstract class ResponseParams { }
        protected string EndpointOverride;
        protected bool IsAuthenticatedRequest { get; set; }
        protected ApiClient ApiClient;
        protected bool InterruptHandleSet { get; set; }
        protected AutoResetEvent InterruptHandle { get; set; }

        protected string Endpoint
        {
            get
            {
                if (!string.IsNullOrEmpty(EndpointOverride))
                    return EndpointOverride;

                return Regex.Replace(GetType().Name, "\\B([A-Z])", "_$1").ToLower();
            }
            set { EndpointOverride = value; }
        }
        
        protected T Get<T>([CallerMemberName] string propertyName = null)
        {
            if (propertyName == null)
                throw new Exception("Property name cannot be null");

            if (!_requestData.ContainsKey(propertyName))
                return default(T);

            return (T)_requestData[propertyName];
        }

        protected void Set<T>(T value, [CallerMemberName] string propertyName = null)
        {
            if (propertyName == null)
                throw new Exception("Property name cannot be null");

            if (_requestData.ContainsKey(propertyName))
                _requestData[propertyName] = value;
            else
                _requestData.Add(propertyName, value);
        }
        
        public TResponse GetResponse(ApiClient apiClient)
        {
            ApiClient = apiClient;
            try
            {
                var responseContent = Task.Run(() =>
                    GetResponseContent(Endpoint, JsonConvert.SerializeObject(_requestData))
                ).Result;

                return JsonConvert.DeserializeObject<TResponse>(responseContent);
            }
            catch (AggregateException ae)
            {
                foreach (var e in ae.InnerExceptions)
                    throw e;
            }

            throw new Exception("An unexpected error occured handling the errors (yes, really).");
        }

        public async Task<TResponse> GetResponseAsync(ApiClient apiClient)
        {
            ApiClient = apiClient;
            var responseContent = await GetResponseContent(Endpoint, JsonConvert.SerializeObject(_requestData));
            return JsonConvert.DeserializeObject<TResponse>(responseContent);
        }
        
        public void SetInteruptHandle(AutoResetEvent interruptHandle)
        {
            InterruptHandleSet = true;
            InterruptHandle = interruptHandle;
        }

        private async Task<string> GetResponseContent(string endpoint, string requestBody)
        {
            Logger.Verbose("Making request to " + endpoint + " endpoint...");

            var restClient = new RestClient(ApiClient.ApiUrl);
            var restRequest = new RestRequest(endpoint, Method.POST) { RequestFormat = DataFormat.Json };
            restRequest.AddParameter("application/json", requestBody, ParameterType.RequestBody);
            
            if (IsAuthenticatedRequest)
            {
                if (string.IsNullOrEmpty(ApiClient.ApiKey))
                    throw new MissingFieldException("API key not provided for authenticated request.");

                if (string.IsNullOrEmpty(ApiClient.PrivateKeyPem))
                    throw new MissingFieldException("Private key not provided");

                var signature = AsymmetricCryptoUtil.CreateSignature(requestBody, ApiClient.PrivateKeyPem);
                restRequest.AddHeader("Authorization", "token " + ApiClient.ApiKey + ":" + signature);
            }

            IRestResponse restResponse = new RestResponse();
            if (InterruptHandleSet)
            {
                // Perform the request asynchronously, but block on the interrupt handle.
                var responseReceived = false;
                var asyncHandle = restClient.ExecuteAsync(restRequest, response =>
                {
                    responseReceived = true;
                    restResponse = response;
                    InterruptHandle.Set();
                });

                InterruptHandle.WaitOne();

                if (!responseReceived)
                {
                    asyncHandle.Abort();
                    throw new RequestException("Network error. Request was interrupted.");
                }
            }
            else
            {
                var cancellationTokenSource = new CancellationTokenSource();
                restResponse = await restClient.ExecuteTaskAsync(restRequest, cancellationTokenSource.Token);
            }
            
            if (restResponse.StatusCode == 0)
                throw new RequestException("Network error. Could not connect to server.");

            if (restResponse.StatusCode != HttpStatusCode.OK)
            {
                ErrorResponse errorResponse;
                try
                {
                    errorResponse = JsonConvert.DeserializeObject<ErrorResponse>(restResponse.Content);
                }
                catch (Exception)
                {
                    throw new NetworkErrorException("Unknown error whilst contacting server");
                }

                switch (restResponse.StatusCode)
                {
                    case HttpStatusCode.BadRequest:
                        throw new BadRequestException(errorResponse.Error);
                    case HttpStatusCode.Unauthorized:
                        throw new UnauthorizedException(errorResponse.Error);
                    case HttpStatusCode.NotFound:
                        throw new NotFoundException(errorResponse.Error);
                    case HttpStatusCode.Conflict:
                        throw new NotFoundException(errorResponse.Error);
                    default:
                        throw new RequestException(errorResponse.Error);
                }
            }

            Logger.Verbose("Received response: " + restResponse.Content);

            return restResponse.Content;
        }
    }
}
