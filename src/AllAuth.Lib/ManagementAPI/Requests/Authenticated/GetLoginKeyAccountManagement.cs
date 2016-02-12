namespace AllAuth.Lib.ManagementAPI.Requests.Authenticated
{
    public sealed class GetLoginKeyAccountManagement : AuthenticatedRequest<GetLoginKeyAccountManagement.ResponseParams>
    {
        public new class ResponseParams
        {
            public string LoginKey;
        }
    }
}
