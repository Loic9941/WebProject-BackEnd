using BLL.IServices;
using DAL.IRepositories;
using Domain;

namespace BLL.Services
{
    public class UserService : IUserService
    {
        protected readonly IGenericRepository<User> _userRepository;
        protected readonly IAuthenticationService _authenticationService;

        public UserService(
            IGenericRepository<User> userRepository,
            IAuthenticationService authenticationService
        )
        {
            _userRepository = userRepository;
            _authenticationService = authenticationService;
        }

        public IEnumerable<User> GetUsers()
        {
            return _userRepository.Get();
        }

        public User? GetById(int Id)
        {
            return _userRepository.GetSingleOrDefault(x => x.Id == Id);
        }

        public void DeleteUser(int id)
        {
            User? user = this.GetById(id);
            if (user == null)
            {
                throw new Exception("User not found");
            }
            if (!_authenticationService.IsAdmin() || _authenticationService.GetUserId() == user.Id)
            {
                throw new Exception("You are not authorized to delete this user");
            }
            _userRepository.Delete(user);
        }

        public IEnumerable<User> GetDeliveryPartners()
        {
            return _userRepository.Get(x => x.Role == "DeliveryPartner");
        }
    }
}
