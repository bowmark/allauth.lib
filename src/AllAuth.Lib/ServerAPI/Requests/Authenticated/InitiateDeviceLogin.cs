namespace AllAuth.Lib.ServerAPI.Requests.Authenticated
{
    public sealed class InitiateDeviceLogin : AuthenticatedRequest<InitiateDeviceLogin.ResponseParams>
    {
        public new class ResponseParams
        {
            public string LoginRequestIdentifier;
        }
    }
}
