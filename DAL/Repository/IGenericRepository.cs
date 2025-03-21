using System.Linq.Expressions;

namespace DAL.Repository
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

        void Add(T entity);

        void Delete(T entity);

        void Update(T entity);

    }
}
