using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Web.Http;
using Library.API.Abstract;
using Library.API.Common.User;

namespace Library.API.Controllers
{
    [AllowAnonymous]
    public class LoginController : ApiController
    {
        private readonly ILoginProvider _authonticationProvider;

        public LoginController(ILoginProvider authProvider)
        {
            _authonticationProvider = authProvider;
        }

        public HttpResponseMessage Post(LogOnModel model)
        {
            try
            {
                if (_authonticationProvider.Login(model) == false)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "User doesn't exist");
                }
                var buffer = Encoding.ASCII.GetBytes(string.Format("{0}:{1}", model.Email, model.Password));
                var authHeader = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(buffer));
                Request.Headers.Authorization = authHeader;
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (Exception exc)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, exc.Message);
            }
        }
    }
}
