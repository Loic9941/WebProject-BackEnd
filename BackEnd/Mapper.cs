
using BLL.IdentityDTOs;
using BLL.ServiceDTOs;
using PL.DTOs;
using PL.Identity;

namespace PL
{
    public static class Mapper
    {
        //Product
        public static ProductModel MapToModel(this ProductServiceDTO product)
        {
            return new ProductModel
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
            };
        }

        public static ProductServiceDTO MapToDTO(this ProductModel product)
        {
                return new ProductServiceDTO
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
            };
        }

        public static LoginDTO MapToDto(this LoginModel login)
        {
            if (login == null) return null;
            return new LoginDTO
            {
                Username = login.Username,
                Password = login.Password

            };
        }

        public static RegisterDTO MapToDto(this RegisterModel register)
        {
            if (register == null) return null;
            return new RegisterDTO
            {
                Username = register.Username,
                Password = register.Password,
                Email = register.Email
            };
        }
    }
}
