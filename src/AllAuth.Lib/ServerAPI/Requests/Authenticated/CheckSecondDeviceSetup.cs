namespace AllAuth.Lib.ServerAPI.Requests.Authenticated
{
    public sealed class CheckSecondDeviceSetup : AuthenticatedRequest<CheckSecondDeviceSetup.ResponseParams>
    {
        public string LoginRequestIdentifier { get { return Get<string>(); } set { Set(value); } }

        public new class ResponseParams
        {
            public bool SecondDeviceCompletedSetup;
        }
    }
}
