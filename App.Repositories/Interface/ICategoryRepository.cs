using App.Repositories.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Repositories.Interface
{
    public interface ICategoryRepository :IGenericRepository<Category>
    {

        Task<Category?> GetCategoryWithProductsAsync(int id);


        IQueryable<Category> GetCategoryWithProducts();






    }
}
