using Domain;
using Microsoft.Identity.Client;

namespace BLL.IServices
{
    public interface IUserService
    {
        public IEnumerable<User> GetUsers();

        public void DeleteUser(int id);

        public IEnumerable<User> GetDeliveryPartners();

        public void BlockUser(int id);

        public void UnblockUser(int id);
    }
}
