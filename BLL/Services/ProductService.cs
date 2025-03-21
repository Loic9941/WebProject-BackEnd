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

        public  IEnumerable<Product> Get()
        {

            return  _productRepository.Get();
        }

        public Product Add(Product product, IFormFile? image)
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
            _productRepository.Add(product);
            return product;
        }

        public Product GetById(int Id)
        {
            return _productRepository.GetSingleOrDefault(x => x.Id == Id);
        }

        public Product Update(int Id, Product product, IFormFile? image)
        {
            
            Product productToUpdate = this.GetById(Id);
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
             _productRepository.Update(productToUpdate);
            return product;
        }

    }
}
