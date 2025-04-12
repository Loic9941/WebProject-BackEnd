using Domain;
using Microsoft.AspNetCore.Http;

namespace BLL.IService
{
    public interface IProductService
    {
        public IEnumerable<Product> Get();
        public Product Add(Product product, IFormFile? image);
        public Product? GetById(int id); 
        public Product Update(int id, Product product, IFormFile? image);
        public void Delete(int id);
    }
}
