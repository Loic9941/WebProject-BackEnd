using BLL.DTOs.InputDTOs;
using Domain;
using Microsoft.AspNetCore.Http;

namespace BLL.IServices
{
    public interface IProductService
    {
        public IEnumerable<Product> Get(ProductFiltersDTO? productFiltersDTO);
        public Product Add(Product product, IFormFile? image);
        public Product? GetById(int id); 
        public Product Update(int id, Product product, IFormFile? image);
        public void Delete(int id);
        public IEnumerable<string> GetCategories();
        public void AddToInvoice(int id);
    }
}
