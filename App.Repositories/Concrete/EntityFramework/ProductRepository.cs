using App.Repositories.Data;
using App.Repositories.Entities;
using App.Repositories.Interface;
using Microsoft.EntityFrameworkCore;

namespace App.Repositories.Concrete.EntityFramework
{
    public class ProductRepository(AppDbContext context) : EfGenericRepository<Product>(context) , IProducRepository
    {
        public  Task<List<Product>> GetTopPriceProductsAsync(int count)
        {
        return  Context.Products.OrderByDescending(x=> x.Price).Take(count).ToListAsync();
        }
    }
}
