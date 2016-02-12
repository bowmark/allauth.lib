namespace AllAuth.Lib.ServerAPI.Requests.Authenticated
{
    public sealed class GetDatabaseMeta : AuthenticatedRequest<GetDatabaseMeta.ResponseParams>
    {
        public string DatabaseIdentifier { get { return Get<string>(); } set { Set(value); } }
        
        public new class ResponseParams
        {
            public int Version;
            public string DataType;
            public string Data;
        }
    }
}
