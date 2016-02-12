namespace AllAuth.Lib.ServerAPI.Requests.Authenticated
{
    public sealed class UpdateDatabaseRecoveryInfo : AuthenticatedRequest<UpdateDatabaseRecoveryInfo.ResponseParams>
    {
        public string PasswordClientHash { get { return Get<string>(); } set { Set(value); } }
        
        public new class ResponseParams
        {
        }
    }
}
