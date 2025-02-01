
using System.Net.NetworkInformation;
using BLL.Identity;
using BLL.ServiceDTOs;
using DAL.Models;
using PL.DTOs;
using PL.Identity;

namespace PL
{
    public static class Mapper
    {
        public static ProductModel MapToDto(this ProductServiceDTO product)
        {
            if (product == null) return null;

            return new ProductModel
            {
                Id = product.Id
            };
        }

        public static LoginModelServiceDTO MapToDto(this LoginModel login)
        {
            if (login == null) return null;
            return new LoginModelServiceDTO
            {
                Username = login.Username,
                Password = login.Password

            };
        }

        public static RegisterModelServiceDTO MapToDto(this RegisterModel register)
        {
            if (register == null) return null;
            return new RegisterModelServiceDTO
            {
                Username = register.Username,
                Password = register.Password,
                Email = register.Email
            };
        }
    }
}
