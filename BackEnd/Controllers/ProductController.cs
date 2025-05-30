using Api;
using BLL.DTOs.InputDTOs;
using BLL.DTOs.OutputDTOs;
using BLL.IServices;
using Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/products")]

    public class ProductController : ControllerBase
    {
        protected readonly IProductService _productService;

        public ProductController(IProductService _productService)
        {
            this._productService = _productService;
        }

        [HttpGet(Name = "GetProducts")]
        public ActionResult<IEnumerable<ProductOutputDTO>> GetProducts(ProductFiltersDTO? productFiltersDTO)
        {
            return Ok(_productService.Get(productFiltersDTO).Select(x => x.MapToDTO()));
        }

        [HttpGet("{id}", Name = "GetProduct")]
        public ActionResult<ProductOutputDTO> GetProduct(int id)
        {
            Product? product = _productService.GetById(id);
            if (product == null)
            {
                return BadRequest();
            }
            return Ok(product.MapToDTO());
        }

        [Authorize(Roles = "Artisan")]
        [HttpPost(Name = "AddProduct")]
        public ActionResult<Product> AddProduct([FromForm] Product product, IFormFile? image)
        {
            try
            {
                return Ok(_productService.Add(product, image).MapToDTO());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize(Roles = "Artisan,Administrator")]
        [HttpPut("{id}", Name = "UpdateProduct")]
        public ActionResult<Product> PutProduct(int Id, [FromForm] Product product, IFormFile? image)
        {
            try
            {
                return Ok(_productService.Update(Id, product, image).MapToDTO());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
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
        [HttpGet("Categories", Name = "Categories")]
        public ActionResult<IEnumerable<string>> GetCategories()
        {
            IEnumerable<string> listCategories = _productService.GetCategories();
            return Ok(listCategories);
        }

        [Authorize(Roles = "Customer")]
        [HttpPost("{productId}/AddToInvoice", Name = "AddToShoppingCart")]
        public ActionResult AddToShoppingCart(int productId)
        {
            try
            {
                _productService.AddToInvoice(productId);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
