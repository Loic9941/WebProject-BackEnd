
using BLL.ServiceDTOs;
using DAL.Models;

namespace BLL
{
    public static class Mapper
    {
        public static ProductServiceDTO MapToDto(this Product product)
        {
            if (product == null) return null;

            return new ProductServiceDTO
            {
                Id = product.Id
            };
        }
    }
}
