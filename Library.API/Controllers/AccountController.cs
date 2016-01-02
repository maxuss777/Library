using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Security;
using Library.API.Common.User;
using Library.API.Providers;

namespace Library.API.Controllers
{
    [AllowAnonymous]
    public class AccountController : ApiController
    {
        [HttpPost]
        public HttpResponseMessage Login(LogOnModel model)
        {
            try
            {
                return Membership.ValidateUser(model.UserName, model.Password) 
                    ? Request.CreateResponse(HttpStatusCode.OK) 
                    : Request.CreateErrorResponse(HttpStatusCode.NotFound, "User doesn't exist");
            }
            catch (Exception exc)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, exc.Message);
            }
        }

        [HttpPost]
        public HttpResponseMessage Register(RegisterModel model)
        {
            try
            {
                MembershipUser membershipUser = 
                    ((CustomMembershipProvider)Membership.Provider).CreateUser(model.Email, model.Password);
               
                return membershipUser != null 
                    ? Request.CreateResponse(HttpStatusCode.OK, membershipUser) 
                    : Request.CreateErrorResponse(HttpStatusCode.NotFound, "User doesn't exist");
            }
            catch (Exception exc)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, exc.Message);
            }
        }
    }
}
