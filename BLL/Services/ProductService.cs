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
        private readonly IGenericRepository<Invoice> _invoiceRepository;
        private readonly IGenericRepository<InvoiceItem> _invoiceItemRepository;

        public ProductService(
            IGenericRepository<Product> productRepository, 
            IAuthenticationService authenticationService,
            IGenericRepository<Invoice> invoiceRepository,
            IGenericRepository<InvoiceItem> invoiceItemRepository
            )
        {
            _productRepository = productRepository;
            _authenticationService = authenticationService;
            _invoiceRepository = invoiceRepository;
            _invoiceItemRepository = invoiceItemRepository;
        }

        public  IEnumerable<Product> Get()
        {
            if (_authenticationService.IsArtisan())
            {
                return _productRepository.Get(x => x.Id == _authenticationService.GetUserId());
            }
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
            var userId = _authenticationService.GetUserId() ?? throw new Exception("User not found");
            product.UserId = userId;
            _productRepository.Add(product);
            return product;
        }

        public Product? GetById(int Id)
        {
            return _productRepository.GetSingleOrDefault(x => x.Id == Id);
        }

        public Product Update(int Id, Product product, IFormFile? image)
        {
            
            Product? productToUpdate = this.GetById(Id);
            if (productToUpdate == null)
            {
                throw new Exception("Product not found");
            }
            if (_authenticationService.IsArtisan() && productToUpdate.UserId != _authenticationService.GetUserId())
            {
                throw new Exception("You are not authorized to update this product");
            }
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

        public void Delete(int id)
        {
            Product? product = this.GetById(id) ?? throw new Exception("Product not found");
            if (_authenticationService.IsArtisan() && product.UserId != _authenticationService.GetUserId())
            {
                throw new Exception("You are not authorized to delete this product");
            }
            _productRepository.Delete(product);
        }
    }
}
