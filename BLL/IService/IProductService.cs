using BLL.DTOs;
using Domain;
using Microsoft.AspNetCore.Http;

namespace BLL.IService
{
    public interface IProductService
    {
        public IEnumerable<Product> Get(ProductFiltersDTO? productFiltersDTO);
        public Product Add(Product product, IFormFile? image);
        public Product? GetById(int id); 
        public Product Update(int id, Product product, IFormFile? image);
        public void Delete(int id);
        public void RateProduct(int id, RateProductDTO rateProductDTO);

        public IEnumerable<string> GetCategories();
    }
}
