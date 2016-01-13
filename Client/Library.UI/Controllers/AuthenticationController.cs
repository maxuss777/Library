using Library.UI.Interfaces;

namespace Library.UI.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Net;
    using System.Web;
    using System.Web.Mvc;
    using Library.UI.Models.Account;

    public class AuthenticationController : Controller
    {
        private readonly IAuthenticationService _loginService;

        public AuthenticationController(IAuthenticationService loginService)
        {
            _loginService = loginService;
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View("Login");
        }

        [HttpPost]
        public ActionResult Login(LoginModel loginModel)
        {
            if (!ModelState.IsValid)
            {
                return View("Login");
            }
            MyAuthorizationHeader authHeader = _loginService.Login(loginModel);

            if (authHeader == null)
            {
                TempData["fail_message"] = "Such user is not registered!";
                return View("Login");
            }

            HttpCookie cookie = new HttpCookie("_auth")
            {
                Value = authHeader.Ticket,
                Expires = DateTime.Now.AddYears(1)
            };
            Response.Cookies.Add(cookie);

            return RedirectToAction("GetAll", "Books");
        }

        [HttpPost]
        public ActionResult LogOut()
        {
            if (Request.Cookies["_auth"] == null)
            {
                return View("Login");
            }

            HttpCookie cookie = new HttpCookie("_auth")
            {
                Expires = DateTime.Now.AddDays(-1),
            };

            Response.Cookies.Add(cookie);

            return RedirectToAction("Login");
        }

        [HttpGet]
        public ActionResult Register()
        {
            return View("Register");
        }

        [HttpPost]
        public ActionResult Register(RegistrationModel registrationModel)
        {
            if (!ModelState.IsValid)
            {
                return View("Register");
            }

            Dictionary<HttpStatusCode, MyAuthorizationHeader> response = _loginService.Register(registrationModel);
            if (response.ContainsKey(HttpStatusCode.Conflict))
            {
                TempData["fail_message"] = "Such user is already registered!";

                return View("Register");
            }

            MyAuthorizationHeader requestOut;
            response.TryGetValue(HttpStatusCode.OK, out requestOut);

            if (requestOut == null)
            {
                TempData["fail_message"] = "Sorry, something gone wrong with your registration(";

                return View("Register");
            }

            HttpCookie cookie = new HttpCookie("_auth")
            {
                Value = requestOut.Ticket,
                Expires = DateTime.Now.AddYears(1)
            };

            Response.Cookies.Add(cookie);
            TempData["succ_message"] = "You have been registered successfully!";

            return RedirectToAction("GetAll", "Books");
        }
    }
}