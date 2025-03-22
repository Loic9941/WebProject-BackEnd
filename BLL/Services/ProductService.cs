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
        private readonly IAuthenticationService _authenticationService;

        public ProductService(IGenericRepository<Product> productRepository, IAuthenticationService authenticationService)
        {
            _productRepository = productRepository;
            _authenticationService = authenticationService;
        }

        public  IEnumerable<Product> Get()
        {
            if (_authenticationService.IsArtisan())
            {
                return _productRepository.Get(x => x.ContactId == _authenticationService.GetContactId());
            }
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
            var contactId = _authenticationService.GetContactId() ?? throw new Exception("User not found");
            product.ContactId = contactId;
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
            Product product = this.GetById(id);
            if(_authenticationService.IsArtisan() && product.ContactId != _authenticationService.GetContactId())
            {
                throw new Exception("You are not authorized to delete this product");
            }
            _productRepository.Delete(product);
        }
    }
}
