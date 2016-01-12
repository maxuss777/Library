namespace Library.UI.Abstract
{
    using System.Collections.Generic;
    using System.Net;
    using Library.UI.Models;
    using Library.UI.Models.Account;

    public interface IAuthenticationService
    {
        MyAuthorizationHeader Login(LoginModel loginModel);
        Dictionary<HttpStatusCode, MyAuthorizationHeader> Register(RegistrationModel regModel);
    }
}