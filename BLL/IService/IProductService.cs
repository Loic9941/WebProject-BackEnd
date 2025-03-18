using Domain;

namespace BLL.IService
{
    public interface IProductService
    {
        public Task<IEnumerable<Product>> GetAsync();
    }
}
