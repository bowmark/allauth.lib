namespace AllAuth.Lib.ServerAPI.Requests.Authenticated
{
    public sealed class SetMessageStatus : AuthenticatedRequest<SetMessageStatus.ResponseParams>
    {
        public string DeviceMessageIdentifier { get { return Get<string>(); } set { Set(value); } }
        public bool ProcessedSuccess { get { return Get<bool>(); } set { Set(value); } }

        public new class ResponseParams
        {
            // No response content
        }
    }
}
