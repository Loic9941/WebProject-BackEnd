using BLL.IService;
using BLL.Services;
using Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Authorize]
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
        public IEnumerable<Product> GetProducts()
        {
            return _productService.Get();
        }

        [HttpGet("{id}", Name = "GetProduct")]
        public Product GetProduct(int id)
        {
            Product? product = _productService.GetById(id);
            if (product == null)
            {
                throw new Exception("Product not found");
            }
            return product;
        }

        [Authorize(Roles = "Artisan")]
        [HttpPost(Name = "AddProduct")]
        public  Product AddProduct([FromForm] Product product, IFormFile? image)
        {
            return  _productService.Add(product, image);
        }

        [Authorize(Roles = "Artisan,Administrator")]
        [HttpPut("{id}", Name = "UpdateProduct")]
        public Product PutProduct(int Id, [FromForm] Product product, IFormFile? image)
        {
            return _productService.Update(Id, product, image);
        }

        [Authorize(Roles = "Artisan,Administrator")]
        [HttpDelete("{id}", Name = "DeleteProduct")]
        public ActionResult DeleteProduct(int id)
        {
            try
            {
                _productService.Delete(id);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
