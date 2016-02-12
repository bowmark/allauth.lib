using System;

namespace AllAuth.Lib.ServerAPI.Requests.Authenticated
{
    public sealed class GetMessageStatus : AuthenticatedRequest<GetMessageStatus.ResponseParams>
    {
        public string MessageIdentifier { get { return Get<string>(); } set { Set(value); } }
        
        public new class ResponseParams
        {
            public bool Processed;
            public DateTime? ProcessedAt;
            public bool ProcessedSuccess;
        }
    }
}
