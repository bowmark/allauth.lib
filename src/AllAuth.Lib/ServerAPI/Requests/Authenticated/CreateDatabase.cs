using System;

namespace AllAuth.Lib.ServerAPI.Requests.Authenticated
{
    public sealed class CreateDatabase : AuthenticatedRequest<CreateDatabase.ResponseParams>
    {
        [Obsolete("Plaintext name of the database should not be sent to the server")]
        public string Name { get { return Get<string>(); } set { Set(value); } }
        
        public new class ResponseParams
        {
            public string DatabaseIdentifier;
        }
    }
}
