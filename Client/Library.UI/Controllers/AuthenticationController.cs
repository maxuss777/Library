﻿using System;
using System.Web;
using System.Web.Mvc;
using Library.UI.Abstract;
using Library.UI.Models;

namespace Library.UI.Controllers
{
    public class AuthenticationController : Controller
    {
        private ILoginServices _loginServices;

        public AuthenticationController(ILoginServices loginServ)
        {
            _loginServices = loginServ;
        }

        [HttpGet]
        public ViewResult Login()
        {
            return View("Login");
        }

        [HttpPost]
        public ActionResult Login(LogInModel logInModel)
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

        [HttpPost]
        public ActionResult LogOut()
        {
            if (Request.Cookies["_auth"] != null)
            {
                var cookie = new HttpCookie("_auth")
                {
                    Expires = DateTime.Now.AddDays(-1),
                };

                Response.Cookies.Add(cookie);
                return RedirectToAction("Login");
            }
            return View("Login");
        }
    }
}