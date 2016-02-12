namespace AllAuth.Lib.ServerAPI.Requests.Authenticated
{
    public sealed class RequestKeyVerificationResponse : AuthenticatedRequest<RequestKeyVerificationResponse.ResponseParams>
    {
        public bool Verified { get { return Get<bool>(); } set { Set(value); } }
        public string Nonce { get { return Get<string>(); } set { Set(value); } }
        public string NonceSigned { get { return Get<string>(); } set { Set(value); } }

        public new class ResponseParams
        {
            
        }
    }
}
