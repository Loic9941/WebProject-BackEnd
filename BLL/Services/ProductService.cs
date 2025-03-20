using BLL.IService;
using Domain;
using DAL.Repository;
using Microsoft.AspNetCore.Http;

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

        public async Task<Product> AddAsync(Product product, IFormFile image)
        {
            if (image != null)
            {
                using (var ms = new MemoryStream())
                {
                    image.CopyTo(ms);
                    product.Image = ms.ToArray();
                }
            }
            product.ContactId = 1;//Fix me with auth user
            await _productRepository.AddAsync(product);
            return product;
        }

        public async Task<Product> GetByIdAsync(int Id)
        {
            return await _productRepository.GetSingleOrDefault(x => x.Id == Id);
        }

        public async Task<Product> UpdateAsync(int Id, Product product, IFormFile image)
        {
            
            Product productToUpdate = await this.GetByIdAsync(Id);
            productToUpdate.Name = product.Name;
            productToUpdate.Description = product.Description;
            productToUpdate.Price = product.Price;
            if (image != null)
            {
                using (var ms = new MemoryStream())
                {
                    image.CopyTo(ms);
                    productToUpdate.Image = ms.ToArray();
                }
            }
            await _productRepository.UpdateAsync(productToUpdate);
            return product;
        }

    }
}
