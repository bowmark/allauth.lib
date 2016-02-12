namespace AllAuth.Lib.ServerAPI.Requests.Authenticated
{
    public sealed class UpdateUser : AuthenticatedRequest<UpdateUser.ResponseParams>
    {
        // No request params at the moment. The originaly purpose for this request has since been changed.
        // Feel free to use this for a new relvant reason.

        public new class ResponseParams
        {
            // No response parameters
        }
    }
}
