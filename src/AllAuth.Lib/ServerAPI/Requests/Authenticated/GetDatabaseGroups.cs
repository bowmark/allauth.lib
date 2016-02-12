using System.Collections.Generic;

namespace AllAuth.Lib.ServerAPI.Requests.Authenticated
{
    public sealed class GetDatabaseGroups : AuthenticatedRequest<GetDatabaseGroups.ResponseParams>
    {
        public string LinkIdentifier { get { return Get<string>(); } set { Set(value); } }
        
        public new class ResponseParams
        {
            public List<ResponseParamsItem> Groups;
        }

        public class ResponseParamsItem
        {
            public string GroupIdentifier;
            public int Version;
        }
    }
}
