using BLL.DTOs;
using BLL.IService;
using Domain;
using Errors;
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
        public ActionResult<IEnumerable<Product>> GetProducts(ProductFiltersDTO? productFiltersDTO)
        {
            return Ok(_productService.Get(productFiltersDTO));
        }

        [HttpGet("{id}", Name = "GetProduct")]
        public ActionResult<Product> GetProduct(int id)
        {
            Product? product = _productService.GetById(id);
            if (product == null)
            {
                return BadRequest();
            }
            return Ok(product);
        }

        [Authorize(Roles = "Artisan")]
        [HttpPost(Name = "AddProduct")]
        public ActionResult<Product> AddProduct([FromForm] Product product, IFormFile? image)
        {
            return  Ok(_productService.Add(product, image));
        }

        [Authorize(Roles = "Artisan,Administrator")]
        [HttpPut("{id}", Name = "UpdateProduct")]
        public ActionResult<Product> PutProduct(int Id, [FromForm] Product product, IFormFile? image)
        {
            return Ok(_productService.Update(Id, product, image));
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

        [Authorize(Roles = "Customer,Administrator,Artisan")]
        [HttpGet("GetCategories",Name = "GetCategories")]
        public ActionResult<IEnumerable<string>> GetCategories()
        {
            IEnumerable<string> listCategories = _productService.GetCategories();
            return Ok(listCategories);
        }
    }
}
