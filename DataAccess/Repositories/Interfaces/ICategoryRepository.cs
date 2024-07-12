using Models.Model;

namespace DataAccess.Repositories
{
    public interface ICategoryRepository : IRepository<Category>
    {
        void Save();
        void Update(Category category);
    }
}
