namespace AllAuth.Lib.ManagementAPI.Requests.Authenticated
{
    public sealed class UpdateRecoveryPassword : AuthenticatedRequest<UpdateRecoveryPassword.ResponseParams>
    {
        public string CurrentRecoveryPasswordClientHash { get { return Get<string>(); } set { Set(value); } }
        public string NewRecoveryPasswordClientHash { get { return Get<string>(); } set { Set(value); } }
        
        public new class ResponseParams
        {
            // No response parameters
        }
    }
}
