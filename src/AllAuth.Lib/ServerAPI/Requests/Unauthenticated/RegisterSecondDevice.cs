namespace AllAuth.Lib.ServerAPI.Requests.Unauthenticated
{
    public sealed class RegisterSecondDevice 
        : RequestAbstract<RegisterSecondDevice.ResponseParams>
    {
        public string LoginRequestIdentifier { get { return Get<string>(); } set { Set(value); } }
        public string PublicKeyPem { get { return Get<string>(); } set { Set(value); } }
        public string DeviceLabel { get { return Get<string>(); } set { Set(value); } }
        public string DeviceType { get { return Get<string>(); } set { Set(value); } }
        public string DeviceSubtype { get { return Get<string>(); } set { Set(value); } }
        
        public new class ResponseParams
        {
            public string UserIdentifier { get; set; }
            public string DeviceIdentifier { get; set; }
            public string ApiKey { get; set; }
            public string Type { get; set; }
            public string Label { get; set; }
            public string Secret { get; set; }
            public string Issuer { get; set; }
            public string Algorithm { get; set; }
            public int Digits { get; set; }
            public int Counter { get; set; }
            public int Period { get; set; }
        }
    }
}
