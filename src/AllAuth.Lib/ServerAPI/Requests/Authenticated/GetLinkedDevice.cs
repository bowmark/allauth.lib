namespace AllAuth.Lib.ServerAPI.Requests.Authenticated
{
    public sealed class GetLinkedDevice : AuthenticatedRequest<GetLinkedDevice.ResponseParams>
    {
        public new class ResponseParams
        {
            public string PublicKeyPem;
        }
    }
}
