namespace AllAuth.Lib.ManagementAPI.Requests
{
    public abstract class AuthenticatedRequest<TResponse> : RequestAbstract<TResponse>
    {
        protected AuthenticatedRequest()
        {
            IsAuthenticatedRequest = true;
        }
    }
}
