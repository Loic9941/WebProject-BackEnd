using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BLL.Services
{
    public interface IAuthenticationService
    {
        Task<TokenModelServiceDTO> Login(LoginModelServiceDTO model);

        Task<bool> RegisterArtisan(RegisterModelServiceDTO model);

        Task<bool> RegisterAdmin(RegisterModelServiceDTO model);

        Task<bool> RegisterCustomer(RegisterModelServiceDTO model);

        Task<bool> RegisterDeliveryPartner(RegisterModelServiceDTO model);
    }
}
