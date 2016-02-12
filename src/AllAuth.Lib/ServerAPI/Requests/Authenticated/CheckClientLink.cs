namespace AllAuth.Lib.ServerAPI.Requests.Authenticated
{
    public sealed class CheckClientLink : AuthenticatedRequest<CheckClientLink.ResponseParams>
    {
        public string LinkIdentifier { get { return Get<string>(); } set { Set(value); } }
        
        public new class ResponseParams
        {
            public bool LinkEstablished;
        }
    }
}
