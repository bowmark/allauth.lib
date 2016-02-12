namespace AllAuth.Lib.ServerAPI.Requests.Authenticated
{
    public sealed class CheckSecondDeviceRecoveryKey : AuthenticatedRequest<CheckSecondDeviceRecoveryKey.ResponseParams>
    {
        public string RecoveryKeyClientHash { get { return Get<string>(); } set { Set(value); } }
        
        public new class ResponseParams
        {
            public bool Success;
        }
    }
}
