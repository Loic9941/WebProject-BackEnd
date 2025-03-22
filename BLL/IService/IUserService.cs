using Domain;

namespace BLL.IService
{
    public interface IUserService
    {
        public IEnumerable<User> GetUsers();
    }
}
