using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using App.Services.Products;

using App.Services.Products;

namespace App.Services.Extensions
{
    public static class ServiceExtensions
    {



        public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IProductServices, ProductService>();

            return services;
        }

    }
}
