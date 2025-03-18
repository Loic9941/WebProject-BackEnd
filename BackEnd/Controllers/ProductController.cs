using BLL.IService;
using Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace PL.Controllers
{
    /*[Authorize]*/
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        protected readonly IProductService _productService;

        public ProductController(IProductService _productService)
        {
            this._productService = _productService;
        }

        
        [HttpGet(Name = "GetProducts")]
        public async Task<IEnumerable<Product>> GetProducts()
        {
            return await _productService.GetAsync();
        }

    }
}
