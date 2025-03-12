using System.Linq.Expressions;

namespace DAL.Repository
{
    public interface IGenericRepository<T> where T : class
    {
        Task<T?> GetSingleOrDefault(
            Expression<Func<T, bool>>? filter = null,
            string includeProperties = ""
        );

        Task<IEnumerable<T>> GetAsync(
            Expression<Func<T, bool>>? filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
            string includeProperties = "");

        Task AddAsync(T entity);

        Task DeleteAsync(T entity);

        Task UpdateAsync(T entity);

    }
}
