using Domain;
using Microsoft.AspNetCore.Http;

namespace BLL.IService
{
    public interface IProductService
    {
        public Task<IEnumerable<Product>> GetAsync();

        public Task<Product> AddAsync(Product product, IFormFile image);

        public Task<Product> GetByIdAsync(int Id);

        public Task<Product> UpdateAsync(int Id, Product product, IFormFile image);
    }
}
