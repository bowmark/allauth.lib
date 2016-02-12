using System.Collections.Generic;

namespace AllAuth.Lib.ServerAPI.Requests.Authenticated
{
    public sealed class SetDatabaseEntryDeviceSecrets : AuthenticatedRequest<SetDatabaseEntryDeviceSecrets.ResponseParams>
    {
        public List<SetDatabaseEntryDeviceSecretsItem> Secrets { get { return Get<List<SetDatabaseEntryDeviceSecretsItem>>(); } set { Set(value); } }

        public class SetDatabaseEntryDeviceSecretsItem
        {
            public string LinkIdentifier;
            public string EntryIdentifier;
            public string SecretIdentifier;
            public string DataType;
            public string Data;
        }

        public new class ResponseParams
        {
        }
    }
}
