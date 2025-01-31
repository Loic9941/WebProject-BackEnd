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
    }
}
