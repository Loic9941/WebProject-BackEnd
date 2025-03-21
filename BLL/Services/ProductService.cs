using BLL.IService;
using Domain;
using DAL.Repository;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace BLL.Services
{
    [Authorize]
    public class ProductService : IProductService
    {
        private readonly IGenericRepository<Product> _productRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ProductService(IGenericRepository<Product> productRepository, IHttpContextAccessor httpContextAccessor)
        {
            _productRepository = productRepository;
            _httpContextAccessor = httpContextAccessor;
        }

        public  IEnumerable<Product> Get()
        {

            return  _productRepository.Get();
        }

        [Authorize(Roles= "Artisan")]
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
            //the contact id is the connected user
            /*var userId = _httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId != null)
            {
                product.ContactId = int.Parse(userId);
            }
            else throw new Exception("User not found");*/
            //Fix me 
            product.ContactId = 2;
            _productRepository.Add(product);
            return product;
        }

        public Product GetById(int Id)
        {
            return _productRepository.GetSingleOrDefault(x => x.Id == Id);
        }

        [Authorize(Roles = "Artisan, Admin")]
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

        [Authorize(Roles = "Artisan, Admin")]
        public void Delete(int id)
        {
            throw new NotImplementedException();
        }
    }
}
