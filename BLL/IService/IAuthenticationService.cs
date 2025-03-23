using BLL.DTOs;

namespace BLL.IService
{
    public interface IAuthenticationService
    {
        public void RegisterUser(RegisterDTO registerDTO);
        public string Login(LoginDTO loginDTO);

        public int? GetUserId();

        public bool IsAdmin();

        public bool IsCustomer();

        public bool IsArtisan();

        public bool IsDeliveryPartner();
    }
}
