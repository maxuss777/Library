using Library.API.Business.Abstract;
using Library.API.Common.User;
using Library.API.DAL.Abstract;

namespace Library.API.Business
{
    public class UserServices : IUserServices
    {
        private readonly IUserRepository _userRepository;

        public UserServices(IUserRepository userRepo)
        {
            _userRepository = userRepo;
        }
        public User Get(string email)
        {
            return email == null  ? null : _userRepository.Get(email);
        }
        public User Create(User user)
        {
            return user == null ? null : _userRepository.Create(user);
        }
    }
}
