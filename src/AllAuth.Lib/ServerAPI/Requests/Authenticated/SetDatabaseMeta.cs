namespace AllAuth.Lib.ServerAPI.Requests.Authenticated
{
    public sealed class SetDatabaseMeta : AuthenticatedRequest<SetDatabaseMeta.ResponseParams>
    {
        public string LinkIdentifier { get { return Get<string>(); } set { Set(value); } }
        public int Version { get { return Get<int>(); } set { Set(value); } }
        public string DataType { get { return Get<string>(); } set { Set(value); } }
        public string Data { get { return Get<string>(); } set { Set(value); } }
        
        public new class ResponseParams
        {
            // No response params
        }
    }
}
