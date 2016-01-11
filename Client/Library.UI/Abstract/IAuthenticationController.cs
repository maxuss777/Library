using System;
using Library.UI.Models;

namespace Library.UI.Abstract
{
    public interface IAuthenticationController
    {
        MyAuthorizationHeader LogIn(LogInModel logInModel);
        MyAuthorizationHeader Register(RegistrationModel regModel);
    }
}
