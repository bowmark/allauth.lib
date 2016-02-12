using System.Collections.Generic;

namespace AllAuth.Lib.ServerAPI.Requests.Authenticated
{
    public sealed class GetDatabaseEntryDeviceSecrets 
        : AuthenticatedRequest<GetDatabaseEntryDeviceSecrets.ResponseParams>
    {
        public string LinkIdentifier { get { return Get<string>(); } set { Set(value); } }
        public string EntryIdentifier { get { return Get<string>(); } set { Set(value); } }
        
        public new class ResponseParams
        {
            public List<ResponseParamsItem> Secrets;
        }

        public class ResponseParamsItem
        {
            public string SecretIdentifier;
            public string DataType;
            public string Data;
        }
    }
}
