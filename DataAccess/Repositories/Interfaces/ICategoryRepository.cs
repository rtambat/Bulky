using Bulky.WebClient.Models;

namespace DataAccess.Repositories
{
    public interface ICategoryRepository : IRepository<Category>
    {
        void Save();
        void Update(Category category);
    }
}
