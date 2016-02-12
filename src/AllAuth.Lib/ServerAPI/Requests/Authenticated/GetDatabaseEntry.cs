namespace AllAuth.Lib.ServerAPI.Requests.Authenticated
{
    public sealed class GetDatabaseEntry : AuthenticatedRequest<GetDatabaseEntry.ResponseParams>
    {
        public string LinkIdentifier { get { return Get<string>(); } set { Set(value); } }
        public string EntryIdentifier { get { return Get<string>(); } set { Set(value); } }
        
        public new class ResponseParams
        {
            public int Version;
            public string DataType;
            public string Data;
        }
    }
}
