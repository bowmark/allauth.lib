using System.Collections.Generic;

namespace AllAuth.Lib.ServerAPI.Requests.Authenticated
{
    public sealed class GetDatabaseEntries : AuthenticatedRequest<GetDatabaseEntries.ResponseParams>
    {
        public string LinkIdentifier { get { return Get<string>(); } set { Set(value); } }
        
        public new class ResponseParams
        {
            public List<ResponseParamsItem> Entries;
        }

        public class ResponseParamsItem
        {
            public string GroupIdentifier;
            public string EntryIdentifier;
            public int Version;
        }
    }
}
