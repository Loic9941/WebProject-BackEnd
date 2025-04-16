using BLL.IService;
using DAL.Repository;
using Domain;

namespace BLL.Services
{
    public class CategoryService : ICategoryService
    {
        protected IGenericRepository<Category> _categoryRepository;

        public CategoryService(IGenericRepository<Category> categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public List<Category> GetCategories()
        {
            return _categoryRepository.Get(
                null, 
                q => q.OrderBy(c => c.Name)
                ).ToList();
        }
    }
}
