namespace AllAuth.Lib.ManagementAPI.Requests.Unauthenticated
{
    public sealed class LoginWithRecoveryKey : RequestAbstract<LoginWithRecoveryKey.ResponseParams>
    {
        public string EmailAddress { get { return Get<string>(); } set { Set(value); } }
        public string RecoveryPasswordClientHash { get { return Get<string>(); } set { Set(value); } }
        
        public new class ResponseParams
        {
            public string UserIdentifier;
            public string ApiKey;
        }
    }
}
