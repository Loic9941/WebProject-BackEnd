using BLL.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PL.DTOs;

namespace PL.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class ProductController : ControllerBase
    {
        protected readonly IProductService _productService;

        public ProductController(IProductService _productService)
        {
            this._productService = _productService;
        }

        
        [HttpGet(Name = "GetProducts")]
        public IEnumerable<ProductModel> GetProducts()
        {
            return _productService.GetProducts().Select(p => p.MapToDto());
        }

    }
}
