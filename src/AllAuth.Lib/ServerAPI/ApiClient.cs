namespace AllAuth.Lib.ServerAPI
{
    public sealed class ApiClient
    {
		internal string ApiUrl { get; private set; }
		internal string ApiKey { get; private set; }
		internal string PrivateKeyPem { get; private set; }

        /// <summary>
        /// Create an unauthenticated API client.
        /// </summary>
        /// <param name="https"></param>
        /// <param name="host"></param>
        /// <param name="port"></param>
        /// <param name="version"></param>
        public ApiClient(bool https, string host, int port, int version)
        {
            ApiUrl = "http" + (https ? "s" : "") + "://" + host + ":" + port + "/v" + version;
        }

        /// <summary>
        /// Create an authenticated API client.
        /// </summary>
        /// <param name="https"></param>
        /// <param name="host"></param>
        /// <param name="port"></param>
        /// <param name="version"></param>
        /// <param name="apiKey"></param>
        /// <param name="privateKeyPem"></param>
        public ApiClient(bool https, string host, int port, int version, string apiKey, string privateKeyPem)
        {
            ApiUrl = "http" + (https ? "s" : "") + "://" + host + ":" + port + "/v" + version;
            ApiKey = apiKey;
            PrivateKeyPem = privateKeyPem;
        }

        //================================================================================
        // DO NOT ADD ANY MORE REQUESTS HERE. PLACE IN "Requests" subnamespace.
        //================================================================================
        
        
        //
        // Authenticated Requests
        //

        public GetUserResponse GetUser(GetUserRequest requestData)
        {
            var apiClientRequest = new ApiClientRequest(ApiUrl, ApiKey, PrivateKeyPem);
            return apiClientRequest.PerformRequest<GetUserResponse>("get_user", requestData);
        }
        
        public GetDevicesKeysResponse GetDeviceKeys(GetDevicesKeysRequest requestData)
        {
            var apiClientRequest = new ApiClientRequest(ApiUrl, ApiKey, PrivateKeyPem);
            return apiClientRequest.PerformRequest<GetDevicesKeysResponse>("get_device_keys", requestData);
        }

        public GetServerInfoResponse GetServerInfo(GetServerInfoRequest requestData)
        {
            var apiClientRequest = new ApiClientRequest(ApiUrl, ApiKey, PrivateKeyPem);
            return apiClientRequest.PerformRequest<GetServerInfoResponse>("get_server_info", requestData);
        }

        public RegisterOtpDeviceResponse RegisterOtpDevice(RegisterOtpDeviceRequest requestData)
        {
            var apiClientRequest = new ApiClientRequest(ApiUrl, ApiKey, PrivateKeyPem);
            return apiClientRequest.PerformRequest<RegisterOtpDeviceResponse>("register_otp_device", requestData);
        }

        public UploadDeviceBackupResponse UploadDeviceBackup(UploadDeviceBackupRequest requestData)
        {
            var apiClientRequest = new ApiClientRequest(ApiUrl, ApiKey, PrivateKeyPem);
            return apiClientRequest.PerformRequest<UploadDeviceBackupResponse>("upload_device_backup", requestData);
        }
    }
}
