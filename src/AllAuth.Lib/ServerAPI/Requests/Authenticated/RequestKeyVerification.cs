namespace AllAuth.Lib.ServerAPI.Requests.Authenticated
{
    public sealed class RequestKeyVerification : AuthenticatedRequest<RequestKeyVerification.ResponseParams>
    {
        public string Nonce { get { return Get<string>(); } set { Set(value); } }
        public string NonceSigned { get { return Get<string>(); } set { Set(value); } }

        public new class ResponseParams
        {
            public string MessageIdentifier;
        }
    }
}
