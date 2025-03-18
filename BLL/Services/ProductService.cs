using BLL.IService;
using Domain;
using DAL.Repository;

namespace BLL.Services
{
    public class ProductService : IProductService
    {
        private readonly IGenericRepository<Product> _productRepository;

        public ProductService(IGenericRepository<Product> productRepository)
        {
            _productRepository = productRepository;
        }

        public  async Task<IEnumerable<Product>> GetAsync()
        {

            return await _productRepository.GetAsync();
        }

    }
}
