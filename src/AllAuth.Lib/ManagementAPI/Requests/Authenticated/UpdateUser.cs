namespace AllAuth.Lib.ManagementAPI.Requests.Authenticated
{
    public sealed class UpdateUser : AuthenticatedRequest<UpdateUser.ResponseParams>
    {
        public string RecoveryPasswordClientHash { get { return Get<string>(); } set { Set(value); } }
        
        public new class ResponseParams
        {
            // No response parameters
        }
    }
}
