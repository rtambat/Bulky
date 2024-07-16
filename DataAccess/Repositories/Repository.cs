using Bulky.WebClient.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace DataAccess.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly DbSet<T> _dbSet;

        public Repository(ApplicationDbContext db)
        {
            _dbSet = db.Set<T>();
        }

        public void Add(T entity)
        {
            _dbSet.Add(entity);
        }

        public T Get(Expression<Func<T, bool>> filter,params string[]? includeTables)
        {
            IQueryable<T> query = _dbSet.Where(filter);
            if (includeTables?.Length > 0)
            {
                foreach (string table in includeTables)
                {
                    query = query.Include(table);
                }
            }
            return query.FirstOrDefault();
        }

        public IEnumerable<T> GetAll(params string[]? includeTables)
        {
            IQueryable<T> query = _dbSet;
            if (includeTables?.Length > 0)
            {
                foreach (string table in includeTables)
                {
                    query = query.Include(table);
                }
            }
            return query.ToList();
        }

        public void Remove(T entity)
        {
            _dbSet.Remove(entity);
        }

        public void RemoveRange(IEnumerable<T> entities)
        {
            _dbSet.RemoveRange(entities);
        }
    }
}
