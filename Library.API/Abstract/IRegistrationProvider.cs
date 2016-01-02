using Library.API.Common.User;

namespace Library.API.Abstract
{
    public interface IRegistrationProvider
    {
        bool Register(RegisterModel model);
    }
}
