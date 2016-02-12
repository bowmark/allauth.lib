namespace AllAuth.Lib.ServerAPI.Requests.Authenticated
{
    public sealed class SendOtpLoginCode : AuthenticatedRequest<SendOtpLoginCode.ResponseParams>
    {
        public string LoginRequestIdentifier { get { return Get<string>(); } set { Set(value); } }
        public string OtpDeviceCode { get { return Get<string>(); } set { Set(value); } }

        public new class ResponseParams
        {
            // No response params for this request
        }
    }
}
