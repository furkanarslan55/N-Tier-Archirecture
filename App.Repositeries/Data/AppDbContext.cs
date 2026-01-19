using App.Repositeries.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace App.Repositeries.Data
{
    public class AppDbContext (DbContextOptions<AppDbContext> option) : DbContext(option)
    {

        public DbSet<Product> Products { get; set; }

        override protected void OnModelCreating(ModelBuilder modelBuilder)
        {
           
           modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());


        }



    }
}
