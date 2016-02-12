namespace AllAuth.Lib.ManagementAPI
{
    public sealed class ApiClient
    {
        internal readonly string ApiUrl;
        internal readonly string ApiKey;
        
        /// <summary>
        /// Create an unauthenticated API client.
        /// </summary>
        /// <param name="https"></param>
        /// <param name="host"></param>
        /// <param name="port"></param>
        /// <param name="version"></param>
        public ApiClient(bool https, string host, int port, int version)
        {
            ApiUrl = "http" + (https?"s":"") + "://" + host + ":" + port + "/v" + version;
        }

        /// <summary>
        /// Create an authenticated API client.
        /// </summary>
        /// <param name="https"></param>
        /// <param name="host"></param>
        /// <param name="port"></param>
        /// <param name="version"></param>
        /// <param name="apiKey"></param>
        public ApiClient(bool https, string host, int port, int version, string apiKey)
        {
            ApiUrl = "http" + (https ? "s" : "") + "://" + host + ":" + port + "/v" + version;
            ApiKey = apiKey;
        }
    }
}
