using System;
using System.Net;
using AllAuth.Lib.APIs;
using AllAuth.Lib.Crypto;
using Newtonsoft.Json;
using RestSharp;

namespace AllAuth.Lib.ServerAPI
{
    internal class ApiClientRequest
    {
        private readonly bool _isAuthorizedRequest;
        private readonly string _apiUrl;
        private readonly string _clientId;
        private readonly string _privateKeyPem;
        
        /// <summary>
        /// Creates an authorized request.
        /// </summary>
        public ApiClientRequest(string apiUrl, string clientId, string privateKeyPem)
        {
            _isAuthorizedRequest = true;
            _apiUrl = apiUrl;
            _clientId = clientId;
            _privateKeyPem = privateKeyPem;
        }

        public T PerformRequest<T>(string endpoint, object requestBody)
        {
            return JsonConvert.DeserializeObject<T>(GetResponseContent(endpoint, requestBody));
        }

        private string GetResponseContent(string endpoint, object requestBody)
        {
            Logger.Verbose("Making request to " + endpoint + " endpoint...");

            var restClient = new RestClient(_apiUrl);
            var restRequest = new RestRequest(endpoint, Method.POST) {RequestFormat = DataFormat.Json};
            var requestBodyString = JsonConvert.SerializeObject(requestBody);
            restRequest.AddParameter("application/json", requestBodyString, ParameterType.RequestBody);
            
            if (_isAuthorizedRequest)
            {
                if (string.IsNullOrEmpty(_clientId))
                    throw new MissingFieldException("Client ID not provided for authenticated request.");

                if (string.IsNullOrEmpty(_privateKeyPem))
                    throw new MissingFieldException("Public key not provided");

                var signature = AsymmetricCryptoUtil.CreateSignature(requestBodyString, _privateKeyPem);
                restRequest.AddHeader("Authorization", "token " + _clientId + ":" + signature);
            }

            var restResponse = restClient.Execute(restRequest);

            if (restResponse.StatusCode == 0)
                throw new NetworkErrorException("Network error. Could not connect to server.");

            if (restResponse.StatusCode != HttpStatusCode.OK)
            {
                ErrorResponse errorResponse;
                try
                {
                    errorResponse = JsonConvert.DeserializeObject<ErrorResponse>(restResponse.Content);
                }
                catch (Exception)
                {
                    throw new RequestException("Unknown error whilst contacting server");
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
                        throw new ConflictException(errorResponse.ErrorCode, errorResponse.Error);
                    default:
                        throw new RequestException(errorResponse.ErrorCode, errorResponse.Error);
                }
            }

            Logger.Verbose("Received response: " + restResponse.Content);

            return restResponse.Content;
        }
    }
}
