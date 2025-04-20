using BLL.IService;
using Domain;
using DAL.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;
using Errors;
using System.Linq.Expressions;
using LinqKit;
using BLL.DTOs.InputDTOs;

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

        public  IEnumerable<Product> Get(ProductFiltersDTO? productFiltersDTO)
        {
            if (_authenticationService.IsArtisan())
            {
                return _productRepository.Get(
                    x => x.UserId == _authenticationService.GetUserId());
            }
            if (productFiltersDTO != null)
            {
                Expression<Func<Product, bool>>? filter = PredicateBuilder.New<Product>(true);
                if (productFiltersDTO.Search is not null && productFiltersDTO.Search!= "")
                {
                    filter = filter.And(x => x.Name.Contains(productFiltersDTO.Search));
                }
                if (productFiltersDTO.Category is not null && productFiltersDTO.Category != "")
                {
                    filter = filter.And(x => x.Category == productFiltersDTO.Category);
                }
                if (productFiltersDTO.MinPrice != null)
                {
                    filter = filter.And(x => x.Price >= productFiltersDTO.MinPrice);
                }
                if (productFiltersDTO.MaxPrice != null && productFiltersDTO.MaxPrice != 0)
                {
                    filter = filter.And(x => x.Price <= productFiltersDTO.MaxPrice);
                }
                return _productRepository.Get(filter);
            }
            return _productRepository.Get();
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
            productToUpdate.Category = product.Category;
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

        public IEnumerable<string> GetCategories()
        {
            return _productRepository.Get().Select(x => x.Category).Distinct();
        }
    }
}
