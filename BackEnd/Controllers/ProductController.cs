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

        [HttpGet("{id}", Name = "GetProduct")]
        public async Task<Product> GetProduct(int id)
        {
            return await _productService.GetByIdAsync(id);
        }

        [HttpPost(Name = "AddProduct")]
        public async Task<Product> AddProduct([FromForm] Product product, IFormFile image)
        {
            return await _productService.AddAsync(product, image);
        }

        [HttpPut("{id}", Name = "UpdateProduct")]
        public async Task<Product> PutProduct(int Id, [FromForm] Product product, IFormFile image)
        {
            return await _productService.UpdateAsync(Id, product, image);
        }


    }
}
