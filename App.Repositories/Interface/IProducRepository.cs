using App.Repositories.Entities;

namespace App.Repositories.Interface
{
    public interface IProducRepository :IGenericRepository<Product>
    {
        public Task <List<Product>> GetTopPriceProductsAsync(int count);

    }
}
