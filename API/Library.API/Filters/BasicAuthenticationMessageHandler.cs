using System;
using System.Net.Http;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Security;

namespace Library.API.Filters
{
    public class BasicAuthenticationMessageHandler : DelegatingHandler
    {
        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var authHeader = request.Headers.Authorization;

            if (authHeader == null)
            {
                return base.SendAsync(request, cancellationToken);
            }

            if (authHeader.Scheme != "Basic")
            {
                return base.SendAsync(request, cancellationToken);
            }

            var encodedUserPass = authHeader.Parameter.Trim();
            var userPass = Encoding.ASCII.GetString(Convert.FromBase64String(encodedUserPass));

            var parts = userPass.Split(":".ToCharArray());
            var username = parts[0];
            var password = parts[1];

            if (!Membership.ValidateUser(username, password))
            {
                return base.SendAsync(request, cancellationToken);
            }

            var identity = new GenericIdentity(username, "Basic");
            var principal = new GenericPrincipal(identity, null);
            Thread.CurrentPrincipal = principal;
            if (HttpContext.Current != null)
            {
                HttpContext.Current.User = principal;
            }

            return base.SendAsync(request, cancellationToken);
        }
    }
}