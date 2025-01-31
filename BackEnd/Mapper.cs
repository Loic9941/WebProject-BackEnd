
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
    }
}
