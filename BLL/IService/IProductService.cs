using Domain;
using Microsoft.AspNetCore.Http;

namespace BLL.IService
{
    public interface IProductService
    {
        public IEnumerable<Product> Get();

        public Product Add(Product product, IFormFile? image);

        public Product GetById(int Id);

        public Product Update(int Id, Product product, IFormFile? image);
    }
}
