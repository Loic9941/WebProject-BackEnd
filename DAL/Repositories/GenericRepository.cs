using System.Data;
using System.Linq.Expressions;
using DAL.IRepositories;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected readonly DBContext _context;

        public GenericRepository(DBContext context)
        {
            _context = context;
        }

        public T? GetSingleOrDefault(
            Expression<Func<T, bool>>? filter = null,
            string includeProperties = ""
        )
        {
            IQueryable<T> query = GetQuery(filter, includeProperties);
            return query.SingleOrDefault();
        }

        private IQueryable<T> GetQuery(
            Expression<Func<T, bool>>? filter = null,
            string includeProperties = ""
        )
        {
            IQueryable<T> query = _context.Set<T>();
            if (filter != null)
            {
                query = query.Where(filter);
            }
            foreach (var includeProperty in includeProperties.Split
                ([','], StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }
            return query;
        }

        public IEnumerable<T> Get(
            Expression<Func<T, bool>>? filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
            string includeProperties = "")
        {

            IQueryable<T> query = GetQuery(filter, includeProperties);
            if (orderBy != null)
            {
                return orderBy(query).ToList();
            }
            else
            {
                return query.ToList();
            }
        }

        public int Add(T entity)
        {
            _context.Set<T>().Add(entity);
            _context.SaveChanges();
            return (int)_context.Entry(entity).Property("Id").CurrentValue;
        }

        public void Delete(T entity)
        {
            _context.Set<T>().Remove(entity);
            _context.SaveChanges();
        }

        public void Update(T entity)
        {
            _context.Set<T>().Update(entity);
            _context.SaveChanges();
        }
    }
}