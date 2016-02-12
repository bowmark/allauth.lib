namespace AllAuth.Lib.ServerAPI.Requests.Authenticated
{
    public sealed class UpdateDeviceRecoveryInfo : AuthenticatedRequest<UpdateDeviceRecoveryInfo.ResponseParams>
    {
        public string PasswordClientHash { get { return Get<string>(); } set { Set(value); } }
        
        public new class ResponseParams
        {
        }
    }
}
