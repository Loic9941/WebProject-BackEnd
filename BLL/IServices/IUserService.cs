using Domain;

namespace BLL.IServices
{
    public interface IUserService
    {
        public IEnumerable<User> GetUsers();

        public void DeleteUser(int id);

        public IEnumerable<User> GetDeliveryPartners();
    }
}
