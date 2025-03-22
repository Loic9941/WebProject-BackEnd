using BLL.IService;
using DAL.Repository;
using Domain;

namespace BLL.Services
{
    public class UserService : IUserService
    {
        protected readonly IGenericRepository<User> _userRepository;

        public UserService(IGenericRepository<User> userRepository)
        {
            _userRepository = userRepository;
        }

        public IEnumerable<User> GetUsers()
        {
            return _userRepository.Get();
        }

    }
}
