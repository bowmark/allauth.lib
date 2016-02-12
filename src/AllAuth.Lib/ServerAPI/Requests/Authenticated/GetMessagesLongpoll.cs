namespace AllAuth.Lib.ServerAPI.Requests.Authenticated
{
    public sealed class GetMessagesLongpoll : AuthenticatedRequest<GetMessagesLongpoll.ResponseParams>
    {
        // No request parameters
        
        public new class ResponseParams
        {
            public bool NewMessages;
        }
    }
}
