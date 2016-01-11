using System;
using Library.UI.Models;

namespace Library.UI.Abstract
{
    public interface ILoginServices
    {
        MyAuthorizationHeader LogIn(LogInModel logInModel);
    }
}
