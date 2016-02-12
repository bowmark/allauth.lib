
namespace AllAuth.Lib.ServerAPI.Requests.Authenticated
{
    public sealed class SetDatabaseGroup : AuthenticatedRequest<SetDatabaseGroup.ResponseParams>
    {
        public string DatabaseIdentifier { get { return Get<string>(); } set { Set(value); } }
        public string GroupIdentifier { get { return Get<string>(); } set { Set(value); } }
        public int    Version { get { return Get<int>(); } set { Set(value); } }
        public string DataType { get { return Get<string>(); } set { Set(value); } }
        public string Data { get { return Get<string>(); } set { Set(value); } }

        public new class ResponseParams
        {
            // No response parameters
        }
    }
}
