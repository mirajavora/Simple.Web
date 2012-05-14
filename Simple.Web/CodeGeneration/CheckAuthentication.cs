namespace Simple.Web.CodeGeneration
{
    internal static class CheckAuthentication
    {
        internal static bool Impl(IRequireAuthentication handler, IContext context)
        {
            var authenticationProvider = SimpleWeb.Configuration.Container.Get<IAuthenticationProvider>() ?? new AuthenticationProvider();
            var user = authenticationProvider.GetLoggedInUser(context);
            if (user == null || !user.IsAuthenticated)
            {
                context.Response.StatusCode = 401;
                context.Response.StatusDescription = "Unauthorized";
                return false;
            }

            handler.CurrentUser = user;
            return true;
        }
    }
}