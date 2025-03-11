
using BLL.ServiceDTOs;
using DAL.Models;

namespace BLL
{
    public static class Mapper
    {
        //Product
        public static ProductServiceDTO MapToDto(this Product product)
        {
            if (product == null) return null;

            return new ProductServiceDTO
            {
                Id = product.Id
            };
        }

        public static Product MapToEntity(this ProductServiceDTO product)
        {
            return new Product
            {
                Id = product.Id
            };
        }
    }
}
