
using BLL.ServiceDTOs;
using DAL.Models;
using PL.DTOs;

namespace PL
{
    public static class Mapper
    {
        public static ProductDTO MapToDto(this ProductServiceDTO product)
        {
            if (product == null) return null;

            return new ProductDTO
            {
                Id = product.Id
            };
        }
    }
}
