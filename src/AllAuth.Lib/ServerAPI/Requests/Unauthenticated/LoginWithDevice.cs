namespace AllAuth.Lib.ServerAPI.Requests.Unauthenticated
{
    public sealed class LoginWithDevice : RequestAbstract<LoginWithDevice.ResponseParams>
    {
        public string LoginRequestIdentifier { get { return Get<string>(); } set { Set(value); } }
        public string ClientToken { get { return Get<string>(); } set { Set(value); } }
        public string PublicKeyPem { get { return Get<string>(); } set { Set(value); } }

        public new class ResponseParams
        {
            public bool Success;
            public string UserIdentifier;
            public string DeviceIdentifier;
            public string ApiKey;
        }
    }
}
