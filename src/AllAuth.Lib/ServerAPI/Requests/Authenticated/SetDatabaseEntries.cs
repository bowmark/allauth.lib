using System.Collections.Generic;

namespace AllAuth.Lib.ServerAPI.Requests.Authenticated
{
    public sealed class SetDatabaseEntries : AuthenticatedRequest<SetDatabaseEntries.ResponseParams>
    {
        public string LinkIdentifier { get { return Get<string>(); } set { Set(value); } }
        public List<EntryItem> Entries  { get { return Get<List<EntryItem>>(); } set { Set(value); } }

        public class EntryItem
        {
            public string EntryIdentifier;
            public string GroupIdentifier;
            public int Version;
            public string DataType;
            public string Data;
        }

        public new class ResponseParams
        {
        }
    }
}
