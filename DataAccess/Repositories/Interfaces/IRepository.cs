using System.Linq.Expressions;

namespace DataAccess.Repositories
{
    public interface IRepository <T> where T : class
    {
        IEnumerable<T> GetAll(params string[]? includeTables);
        T Get(Expression<Func<T, bool>> filter, params string[]? includeTables);
        void Add(T entity);
        void Remove(T entity);
        void RemoveRange(IEnumerable<T> entities);
    }
}
