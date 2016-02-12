namespace AllAuth.Lib.ServerAPI.Requests.Authenticated
{
    public sealed class CompleteLink : AuthenticatedRequest<CompleteLink.ResponseParams>
    {
        public string LinkIdentifier { get { return Get<string>(); } set { Set(value); } }
        
        public new class ResponseParams
        {
            // No response params for this request
        }
    }
}
