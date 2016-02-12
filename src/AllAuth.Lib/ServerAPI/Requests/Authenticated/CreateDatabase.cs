namespace AllAuth.Lib.ServerAPI.Requests.Authenticated
{
    public sealed class CreateDatabase : AuthenticatedRequest<CreateDatabase.ResponseParams>
    {
        public string Name { get { return Get<string>(); } set { Set(value); } }
        
        public new class ResponseParams
        {
            public string DatabaseIdentifier;
        }
    }
}
