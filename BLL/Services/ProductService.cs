using BLL.IService;
using Domain;
using DAL.Repository;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using BLL.DTOs;
using Microsoft.AspNetCore.Http.HttpResults;
using Errors;

namespace BLL.Services
{
    [Authorize]
    public class ProductService : IProductService
    {
        private readonly IGenericRepository<Product> _productRepository;
        private readonly IAuthenticationService _authenticationService;
        private readonly IGenericRepository<InvoiceItem> _invoiceItemRepository;
        private readonly IGenericRepository<Rating> _ratingRepository;

        public ProductService(
            IGenericRepository<Product> productRepository, 
            IAuthenticationService authenticationService,
            IGenericRepository<InvoiceItem> invoiceItemRepository,
            IGenericRepository<Rating> ratingRepository
            )
        {
            _productRepository = productRepository;
            _authenticationService = authenticationService;
            _invoiceItemRepository = invoiceItemRepository;
            _ratingRepository = ratingRepository;
        }

        public  IEnumerable<Product> Get()
        {
            if (_authenticationService.IsArtisan())
            {
                return _productRepository.Get(x => x.UserId == _authenticationService.GetUserId());
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

        public void RateProduct(int id, RateProductDTO rateProductDTO)
        {
            Product? product = this.GetById(id) ?? throw new Exception("Product not found");
            IEnumerable<InvoiceItem> invoiceItem = _invoiceItemRepository.Get(
                x => x.ProductId == id && 
                x.Invoice.UserId == _authenticationService.GetUserId()
            );
            if (invoiceItem.Count() == 0)
            {
                throw new Exception("You must have purchased this product to rate it");
            }
            //check if there is already a rating for this user and this product
            _ratingRepository.GetSingleOrDefault(
                x => x.ProductId == id &&
                x.UserId == _authenticationService.GetUserId()
            );
            if (product.Ratings.Any(x => x.UserId == _authenticationService.GetUserId()))
            {
                throw new RatingConflict();
            }
            product.Ratings.Add(new Rating
            {
                ProductId = id,
                UserId = _authenticationService.GetUserId() ?? throw new Exception("User not found"),
                Rate = rateProductDTO.Rate,
                Comment = rateProductDTO.Comment,
            });
            _productRepository.Update(product);
        }
    }
}
