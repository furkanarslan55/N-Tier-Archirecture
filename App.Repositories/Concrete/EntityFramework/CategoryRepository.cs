using App.Repositories.Data;
using App.Repositories.Entities;
using App.Repositories.Interface;
using Microsoft.EntityFrameworkCore;
using System;

namespace App.Repositories.Concrete.EntityFramework
{
    public class CategoryRepository(AppDbContext context) : EfGenericRepository<Category>(context), ICategoryRepository
    {

        public async Task<Category?> GetCategoryWithProductsAsync(int id)
        {


            return await context.Categories.Include(x => x.Products).FirstOrDefaultAsync(x => x.Id == id);



        }

        public IQueryable<Category> GetCategoryWithProducts()

         {

         return  context.Categories.Include(x => x.Products).AsQueryable();

         }

    }


}
