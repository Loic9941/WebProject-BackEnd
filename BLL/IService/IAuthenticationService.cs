using BLL.IdentityDTOs;

namespace BLL.IService
{
    public interface IAuthenticationService
    {
        Task<TokenDTO> Login(LoginDTO model);

        Task<bool> RegisterArtisan(RegisterDTO model);

        Task<bool> RegisterAdmin(RegisterDTO model);

        Task<bool> RegisterCustomer(RegisterDTO model);

        Task<bool> RegisterDeliveryPartner(RegisterDTO model);
    }
}
