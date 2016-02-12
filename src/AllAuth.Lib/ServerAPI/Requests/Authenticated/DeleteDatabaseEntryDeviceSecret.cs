namespace AllAuth.Lib.ServerAPI.Requests.Authenticated
{
    public sealed class DeleteDatabaseEntryDeviceSecret 
        : AuthenticatedRequest<DeleteDatabaseEntryDeviceSecret.ResponseParams>
    {
        public string LinkIdentifier { get { return Get<string>(); } set { Set(value); } }
        public string SecretIdentifier { get { return Get<string>(); } set { Set(value); } }

        public new class ResponseParams
        {
        }
    }
}
