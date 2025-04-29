using BLL.IService;
using Domain;
using DAL.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;
using System.Linq.Expressions;
using LinqKit;
using BLL.DTOs.InputDTOs;
using BLL.DTOs.OutputDTOs;

namespace BLL.Services
{
    [Authorize]
    public class ProductService : IProductService
    {
        private readonly IGenericRepository<Product> _productRepository;
        private readonly IAuthenticationService _authenticationService;
        private readonly IGenericRepository<InvoiceItem> _invoiceItemRepository;
        private readonly IGenericRepository<Rating> _ratingRepository;
        private readonly IGenericRepository<Invoice> _invoiceRepository;

        public ProductService(
            IGenericRepository<Product> productRepository, 
            IAuthenticationService authenticationService,
            IGenericRepository<InvoiceItem> invoiceItemRepository,
            IGenericRepository<Rating> ratingRepository,
            IGenericRepository<Invoice> invoiceRepository
            )
        {
            _productRepository = productRepository;
            _authenticationService = authenticationService;
            _invoiceItemRepository = invoiceItemRepository;
            _ratingRepository = ratingRepository;
            _invoiceRepository = invoiceRepository;
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
                filter = filter.And(x => x.Available == true);
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

                Func<IQueryable<Product>, IOrderedQueryable<Product>>? OrderBy = null;
                if(productFiltersDTO.OrderBy is not null)
                {
                    switch (productFiltersDTO.OrderBy)
                    {
                        case "name":
                            OrderBy = q => q.OrderBy(x => x.Name);
                            break;
                        case "nameDesc":
                            OrderBy = q => q.OrderByDescending(x => x.Name);
                            break;
                        case "price":
                            OrderBy = q => q.OrderBy(x => x.Price);
                            break;
                        case "priceDesc":
                            OrderBy = q => q.OrderByDescending(x => x.Price);
                            break;
                        case "createdAt":
                            OrderBy = q => q.OrderBy(x => x.CreatedAt);
                            break;
                        case "createdAtDesc":
                            OrderBy = q => q.OrderByDescending(x => x.CreatedAt);
                            break;
                    }
                }

                return _productRepository.Get(filter, OrderBy);
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
            productToUpdate.Available = product.Available;
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

        public void AddToInvoice(int id)
        {
            int invoiceId = 0;

            Product? product = this._productRepository.GetSingleOrDefault(x => x.Id == id) ?? throw new Exception("Product not found");
            var userId = _authenticationService.GetUserId() ?? throw new Exception("User not found");
            Invoice? invoice = _invoiceRepository.GetSingleOrDefault(x => x.Status == "Pending" && x.UserId == userId);
            if (invoice == null)
            {
                invoice = new Invoice { UserId = userId, Status = "pending", CreatedAt = DateTime.Now };
                invoiceId = _invoiceRepository.Add(invoice);
            }
            else
            {
                invoiceId = invoice.Id;
            }
            //check if there is already an invoice item with the same product
            var existingInvoiceItem = _invoiceItemRepository.GetSingleOrDefault(x => x.InvoiceId == invoiceId && x.ProductId == id);
            if (existingInvoiceItem == null)
            {
                _invoiceItemRepository.Add(new InvoiceItem
                {
                    InvoiceId = invoiceId,
                    ProductId = id,
                    UnitPrice = product.Price,
                    Quantity = 1,
                    Name = product.Name,
                    UserId = userId,
                });
            }
            else
            {
                existingInvoiceItem.UnitPrice = product.Price; // to be sure to have the last price
                existingInvoiceItem.Quantity += 1;
                _invoiceItemRepository.Update(existingInvoiceItem);
            }
        }
    }
}
