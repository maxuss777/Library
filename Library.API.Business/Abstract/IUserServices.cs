using Library.API.Common.User;

namespace Library.API.Business.Abstract
{
    public interface IUserServices
    {
        User Get(string email);
        User Create(User user);
    }
}
