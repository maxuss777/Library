using System;
using System.Web;
using System.Web.Mvc;
using Library.UI.Abstract;
using Library.UI.Models;

namespace Library.UI.Controllers
{
    public class LoginController : Controller
    {
        private ILoginServices _loginServices;

        public LoginController(ILoginServices loginServ)
        {
            _loginServices = loginServ;
        }

        [HttpGet]
        public ViewResult Get()
        {
            return View("Login");
        }

        [HttpPost]
        public ActionResult Get(LogInModel logInModel)
        {
            if (!ModelState.IsValid)
            {
                return View("Login");
            }
            var authHeader = _loginServices.LogIn(logInModel);

            if (authHeader==null)
            {
                return View("Login");
            }
            var cokie = new HttpCookie("_auth")
            {
                Value = authHeader.Ticket, 
                Expires = DateTime.Now.AddYears(1)
            };
            Response.Cookies.Add(cokie);
            return RedirectToAction("GetAll","Books");
        }
    }
}