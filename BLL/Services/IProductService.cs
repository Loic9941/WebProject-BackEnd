
using DAL.Models;

namespace BLL.Services
{
    public interface IProductService
    {
        IEnumerable<Product> GetProducts();
    }
}
