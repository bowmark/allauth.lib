namespace AllAuth.Lib.ManagementAPI.Requests.Unauthenticated
{
    public sealed class RegisterComplete : RequestAbstract<RegisterComplete.ResponseParams>
    {
        public string RegistrationIdentifier { get { return Get<string>(); } set { Set(value); } }
        public string ClientToken { get { return Get<string>(); } set { Set(value); } }
        public string EmailToken { get { return Get<string>(); } set { Set(value); } }
        public string RecoveryPasswordClientHash { get { return Get<string>(); } set { Set(value); } }
        
        public new class ResponseParams
        {
            public string UserIdentifier;
            public string ApiKey;
            public string ServerRegistrationIdentfier;
            public string ServerClientToken;
        }
    }
}
