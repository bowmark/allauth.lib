namespace AllAuth.Lib.ServerAPI.Requests.Authenticated
{
    public sealed class GetDatabaseGroup : AuthenticatedRequest<GetDatabaseGroup.ResponseParams>
    {
        public string LinkIdentifier { get { return Get<string>(); } set { Set(value); } }
        public string GroupIdentifier { get { return Get<string>(); } set { Set(value); } }
        
        public new class ResponseParams
        {
            public int Version;
            public string DataType;
            public string Data;
        }
    }
}
