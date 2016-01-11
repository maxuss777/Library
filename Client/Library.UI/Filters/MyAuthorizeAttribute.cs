using System;
using System.Web.Mvc;

namespace Library.UI.Filters
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = true, AllowMultiple = true)]
    public class MyAuthorizeAttribute : AuthorizeAttribute
    {
        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            if (filterContext.HttpContext.Request.Cookies.Get("_auth")==null)
            {
                filterContext.Result = new RedirectResult("~/Authentication/Login");
            }
        }
    }
}