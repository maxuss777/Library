namespace Library.API.Filters
{
    using System;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Security.Principal;
    using System.Text;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Web;
    using System.Web.Security;

    public class AuthenticationHandler : DelegatingHandler
    {
        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            AuthenticationHeaderValue authHeader = request.Headers.Authorization;

            if (authHeader == null)
            {
                return base.SendAsync(request, cancellationToken);
            }

            if (authHeader.Scheme != "Basic")
            {
                return base.SendAsync(request, cancellationToken);
            }

            string encodedUserPass = authHeader.Parameter.Trim();
            string userPass = Encoding.ASCII.GetString(Convert.FromBase64String(encodedUserPass));

            string[] parts = userPass.Split(":".ToCharArray());
            string username = parts[0];
            string password = parts[1];

            if (!Membership.ValidateUser(username, password))
            {
                return base.SendAsync(request, cancellationToken);
            }

            GenericIdentity identity = new GenericIdentity(username, "Basic");
            GenericPrincipal principal = new GenericPrincipal(identity, null);
            Thread.CurrentPrincipal = principal;
            
            if (HttpContext.Current != null)
            {
                HttpContext.Current.User = principal;
            }

            return base.SendAsync(request, cancellationToken);
        }
    }
}