namespace AllAuth.Lib.ServerAPI.Requests.Unauthenticated
{
    public sealed class LoginWithDatabaseRecoveryKey : RequestAbstract<LoginWithDatabaseRecoveryKey.ResponseParams>
    {
        public string EmailAddress { get { return Get<string>(); } set { Set(value); } }
        public string RecoveryPasswordClientHash { get { return Get<string>(); } set { Set(value); } }
        public string DeviceLabel { get { return Get<string>(); } set { Set(value); } }
        public string DeviceType { get { return Get<string>(); } set { Set(value); } }
        public string DeviceSubtype { get { return Get<string>(); } set { Set(value); } }
        public string PublicKeyPem { get { return Get<string>(); } set { Set(value); } }

        public new class ResponseParams
        {
            public string UserIdentifier;
            public string DeviceIdentifier;
            public string ApiKey;
            public bool SecondDeviceSetupRequired;
        }
    }
}
