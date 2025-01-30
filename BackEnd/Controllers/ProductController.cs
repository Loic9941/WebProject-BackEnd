using BackEnd;
using BLL.Services;
using DAL.Models;
using Microsoft.AspNetCore.Mvc;
using PL.DTOs;

namespace PL.Controllers
{
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
        public IEnumerable<Product> Get() //Fix me
        {
            return _productService.GetProducts();
        }

    }
}
