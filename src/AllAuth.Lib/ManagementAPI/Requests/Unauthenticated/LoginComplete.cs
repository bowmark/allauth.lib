namespace AllAuth.Lib.ManagementAPI.Requests.Unauthenticated
{
    public sealed class LoginComplete : RequestAbstract<LoginComplete.ResponseParams>
    {
        public string LoginRequestIdentifier { get { return Get<string>(); } set { Set(value); } }
        public string ClientToken { get { return Get<string>(); } set { Set(value); } }
        public string SecondToken { get { return Get<string>(); } set { Set(value); } }
        
        public new class ResponseParams
        {
            public string UserIdentifier;
            public string ApiKey;
        }
    }
}
