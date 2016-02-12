using System;
using System.Collections.Generic;

namespace AllAuth.Lib.ManagementAPI.Requests.Authenticated
{
    public sealed class GetUser : AuthenticatedRequest<GetUser.ResponseParams>
    {
        // No request parameters

        public new class ResponseParams
        {
            public bool IsSubscribed;
            public bool InTrial;
            public DateTime? TrialEndsAt;
            public List<ResponseServerItem> Servers;
        }

        public class ResponseServerItem
        {
            public string Identifier;
            public string Label;
            public bool HttpsEnabled;
            public string Hostname;
            public int Port;
            public int ApiVersion;
        }
    }
}
