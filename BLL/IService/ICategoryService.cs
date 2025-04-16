using Domain;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace BLL.IService
{
    public interface ICategoryService
    {
        public List<Category> GetCategories();
    }
}
