
using BLL.ServiceDTOs;

namespace BLL.Services
{
    public interface IProductService
    {
        IEnumerable<ProductServiceDTO> GetProducts();
    }
}
