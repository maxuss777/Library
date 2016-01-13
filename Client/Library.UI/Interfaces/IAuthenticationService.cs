using System.Collections.Generic;
using System.Net;
using Library.UI.Models.Account;

namespace Library.UI.Interfaces
{
    public interface IAuthenticationService
    {
        MyAuthorizationHeader Login(LoginModel loginModel);
        Dictionary<HttpStatusCode, MyAuthorizationHeader> Register(RegistrationModel regModel);
    }
}