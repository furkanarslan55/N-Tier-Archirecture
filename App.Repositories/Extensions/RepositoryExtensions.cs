using App.Repositories.Concrete.EntityFramework;
using App.Repositories.Data;
using App.Repositories.Interface;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace App.Repositories.Extensions
{
    public  static class RepositoryExtensions
    {


        public static IServiceCollection AddRepositories(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AppDbContext>((sp, options) =>
            {
                var conn = sp.GetRequiredService<IOptions<ConnectionStringOption>>().Value;

                options.UseSqlServer(conn.SqlServer, sql =>
                {
                    sql.MigrationsAssembly(typeof(RepositoryAssembly).Assembly.FullName);
                });
            });

            services.AddScoped<IProducRepository, ProductRepository>();
            services.AddScoped(typeof(IGenericRepository<>), typeof(EfGenericRepository<>));
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            return services;
        }



    }
}
