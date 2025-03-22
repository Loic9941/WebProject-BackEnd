using BLL.IService;
using Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
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
        public async Task<Product> GetProduct(int id)
        {
            return _productService.GetById(id);
        }

        [HttpPost(Name = "AddProduct")]
        public  Product AddProduct([FromForm] Product product, IFormFile? image)
        {
            return  _productService.Add(product, image);
        }

        [HttpPut("{id}", Name = "UpdateProduct")]
        public Product PutProduct(int Id, [FromForm] Product product, IFormFile? image)
        {
            return _productService.Update(Id, product, image);
        }

        [HttpDelete("{id}", Name = "DeleteProduct")]
        public void DeleteProduct(int id)
        {
            _productService.Delete(id);
        }


    }
}
