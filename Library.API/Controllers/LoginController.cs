using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Security;
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
                FormsAuthentication.SetAuthCookie(model.UserName, model.RememberMe);
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (Exception exc)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, exc.Message);
            }
        }
    }
}
