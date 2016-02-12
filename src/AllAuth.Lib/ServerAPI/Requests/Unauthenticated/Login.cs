namespace AllAuth.Lib.ServerAPI.Requests.Unauthenticated
{
    public sealed class Login : RequestAbstract<Login.ResponseParams>
    {
        public string EmailAddress { get { return Get<string>(); } set { Set(value); } }
        public string Password { get { return Get<string>(); } set { Set(value); } }
        public string DeviceLabel { get { return Get<string>(); } set { Set(value); } }
        public string DeviceType { get { return Get<string>(); } set { Set(value); } }
        public string DeviceSubtype { get { return Get<string>(); } set { Set(value); } }

        public new class ResponseParams
        {
            public string LoginRequestIdentifier;
            public string ClientToken;
            public bool SecondDeviceSetupRequired;
        }
    }
}
