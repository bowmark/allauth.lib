namespace AllAuth.Lib.ServerAPI.Requests.Authenticated
{
    public sealed class ConfirmSecondDevice : AuthenticatedRequest<ConfirmSecondDevice.ResponseParams>
    {
        public string OtpCode { get { return Get<string>(); } set { Set(value); } }
        
        public new class ResponseParams
        {
            // No response content
        }
    }
}
