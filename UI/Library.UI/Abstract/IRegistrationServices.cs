using System;
using Library.UI.Models;

namespace Library.UI.Abstract
{
    public interface IRegistrationServices
    {
        MyAuthorizationHeader Register(RegistrationModel regModel);
    }
}
