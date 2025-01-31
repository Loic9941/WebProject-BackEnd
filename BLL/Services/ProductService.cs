using BLL.ServiceDTOs;
using DAL.Repository;

namespace BLL.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public IEnumerable<ProductServiceDTO> GetProducts()
        {
            return _productRepository.Get().Select(p => p.MapToDto());
        }

    }
}
