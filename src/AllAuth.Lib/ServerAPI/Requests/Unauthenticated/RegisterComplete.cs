namespace AllAuth.Lib.ServerAPI.Requests.Unauthenticated
{
    public sealed class RegisterComplete : RequestAbstract<RegisterComplete.ResponseParams>
    {
        public string RegisterRequestIdentifier { get { return Get<string>(); } set { Set(value); } }
        public string ClientToken { get { return Get<string>(); } set { Set(value); } }
        public string RecoveryPasswordClientHash { get { return Get<string>(); } set { Set(value); } }
        public string DeviceLabel { get { return Get<string>(); } set { Set(value); } }
        public string DeviceType { get { return Get<string>(); } set { Set(value); } }
        public string DeviceSubtype { get { return Get<string>(); } set { Set(value); } }
        public string PublicKeyPem { get { return Get<string>(); } set { Set(value); } }
        
        public new class ResponseParams
        {
            public string UserIdentifier { get; set; }
            public string DeviceIdentifier { get; set; }
            public string ApiKey { get; set; }
        }
    }
}
