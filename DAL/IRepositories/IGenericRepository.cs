using System.Linq.Expressions;

namespace DAL.IRepositories
{
    public interface IGenericRepository<T> where T : class
    {
        T? GetSingleOrDefault(
            Expression<Func<T, bool>>? filter = null,
            string includeProperties = ""
        );

        IEnumerable<T> Get(
            Expression<Func<T, bool>>? filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
            string includeProperties = "");

        int Add(T entity);

        void Delete(T entity);

        void Update(T entity);

    }
}
