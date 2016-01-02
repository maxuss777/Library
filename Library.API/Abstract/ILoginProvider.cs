using Library.API.Common.User;

namespace Library.API.Abstract
{
    public interface ILoginProvider
    {
        bool Login(LogOnModel model);
    }
}
