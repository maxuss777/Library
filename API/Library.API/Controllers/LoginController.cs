using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Web.Http;
using System.Web.Security;
using Library.API.Common.Member;

namespace Library.API.Controllers
{
    [AllowAnonymous]
    public class LoginController : ApiController
    {
        public HttpResponseMessage Post(LogOnModel model)
        {
            try
            {
                if (Membership.ValidateUser(model.Email,model.Password) == false)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Member doesn't exist");
                }
                var buffer = Encoding.ASCII.GetBytes(string.Format("{0}:{1}", model.Email, model.Password));
                var authHeader = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(buffer));
                return Request.CreateResponse(HttpStatusCode.OK,
                    new AuthorizationTicket {Ticket = authHeader.ToString()});
            }
            catch (Exception exc)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, exc.Message);
            }
        }
    }
}
