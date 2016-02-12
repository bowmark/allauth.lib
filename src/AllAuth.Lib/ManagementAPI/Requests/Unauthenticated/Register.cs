namespace AllAuth.Lib.ManagementAPI.Requests.Unauthenticated
{
    public sealed class Register : RequestAbstract<Register.ResponseParams>
    {
        public string EmailAddress { get { return Get<string>(); } set { Set(value); } }
        public string RecoveryPasswordClientHash { get { return Get<string>(); } set { Set(value); } }
        
        public new class ResponseParams
        {
            public string RegistrationIdentifier;
            public string ClientToken;
        }
    }
}
