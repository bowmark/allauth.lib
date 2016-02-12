namespace AllAuth.Lib.ServerAPI.Requests
{
    public abstract class AuthenticatedRequest<TResponse> : RequestAbstract<TResponse>
    {
        protected AuthenticatedRequest()
        {
            IsAuthenticatedRequest = true;
        }
    }
}
