using App.Repositories.Concrete.EntityFramework;
using App.Repositories.Data;
using App.Repositories.Interface;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace App.Repositories.Extensions
{
    public  static class RepositoryExtensions
    {


        public static IServiceCollection AddRepositories(this IServiceCollection services ,IConfiguration configuration)
        {
            services.AddDbContext<AppDbContext>(options =>
            {
                var connectionStrings = configuration.GetSection(ConnectionStringOption.Key).Get<ConnectionStringOption>();

                options.UseSqlServer(connectionStrings!.SqlServer, sqlServerOptionsAction =>
                {

                    sqlServerOptionsAction.MigrationsAssembly(typeof(RepositoryAssembly).Assembly.FullName);

                });
            });

            services.AddScoped<IProducRepository , ProductRepository>();
            services.AddScoped(typeof(IGenericRepository<>), typeof(EfGenericRepository<>));
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            return services;
        }




    }
}
