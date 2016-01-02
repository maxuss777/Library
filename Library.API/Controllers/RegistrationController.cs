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
    public class RegistrationController : ApiController
    {
        private readonly IRegistrationProvider _redistrationProvider;

        public RegistrationController(IRegistrationProvider regProvider)
        {
            _redistrationProvider = regProvider;
        }

        public HttpResponseMessage Post(RegisterModel model)
        {
            try
            {
                if (_redistrationProvider.Register(model) == false)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "User hasn't been registered");
                }
                FormsAuthentication.SetAuthCookie(model.UserName, false);
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (Exception exc)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, exc.Message);
            }
        }
    }
}