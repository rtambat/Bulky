using Models.Model;

namespace DataAccess.Repositories.Interfaces
{
    public interface IProductRepository : IRepository<Product>
    {
        void Save();
        void Update(Product product);
    }
}
