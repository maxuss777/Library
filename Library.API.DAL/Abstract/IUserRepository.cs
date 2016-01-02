using System;
using Library.API.Common.User;

namespace Library.API.DAL.Abstract
{
    public interface IUserRepository
    {
        User Get(string email);
        User Create(User user);
    }
}