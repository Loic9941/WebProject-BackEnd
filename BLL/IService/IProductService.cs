using BLL.ServiceDTOs;

namespace BLL.IService
{
    public interface IProductService
    {
        public Task<IEnumerable<ProductServiceDTO>> GetAsync();
    }
}
