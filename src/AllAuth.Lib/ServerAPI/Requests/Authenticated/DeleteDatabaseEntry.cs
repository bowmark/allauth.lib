namespace AllAuth.Lib.ServerAPI.Requests.Authenticated
{
    public sealed class DeleteDatabaseEntry : AuthenticatedRequest<DeleteDatabaseEntry.ResponseParams>
    {
        public string LinkIdentifier { get { return Get<string>(); } set { Set(value); } }
        public string EntryIdentifier { get { return Get<string>(); } set { Set(value); } }

        public new class ResponseParams
        {
        }
    }
}
