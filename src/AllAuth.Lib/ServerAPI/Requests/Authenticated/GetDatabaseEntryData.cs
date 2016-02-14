using System.Collections.Generic;

namespace AllAuth.Lib.ServerAPI.Requests.Authenticated
{
    public sealed class GetDatabaseEntryData : AuthenticatedRequest<GetDatabaseEntryData.ResponseParams>
    {
        public string LinkIdentifier { get { return Get<string>(); } set { Set(value); } }
        public List<string> EntryIdentifiers { get { return Get<List<string>>(); } set { Set(value); } }
        
        public new class ResponseParams
        {
            public List<ResponseParamsItem> EntriesData;
        }

        public class ResponseParamsItem
        {
            public int Version;
            public string DataType;
            public string Data;
        }
    }
}
