namespace AllAuth.Lib.ManagementAPI.Requests.Unauthenticated
{
    public sealed class Login : RequestAbstract<Login.ResponseParams>
    {
        public string EmailAddress { get { return Get<string>(); } set { Set(value); } }
        
        public new class ResponseParams
        {
            public string LoginRequestIdentifier;
            public string ClientToken;
            public string SecondTokenChannel;
        }
    }
}
