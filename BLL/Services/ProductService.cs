using BLL.IService;
using BLL.ServiceDTOs;
using DAL.Models;
using DAL.Repository;

namespace BLL.Services
{
    public class ProductService : IProductService
    {
        private readonly GenericRepository<Product> _productRepository;

        public ProductService(GenericRepository<Product> productRepository)
        {
            _productRepository = productRepository;
        }

        public  async Task<IEnumerable<ProductServiceDTO>> GetAsync()
        {

            return (await _productRepository.GetAsync()).Select(p => p.MapToDto());
        }

    }
}
