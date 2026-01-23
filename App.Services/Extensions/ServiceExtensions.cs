using App.Services.Categories;
using App.Services.Products;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace App.Services.Extensions
{
    public static class ServiceExtensions
    {



        public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IProductServices, ProductService>();
            services.AddScoped<ICategoryServices, CategoryService>();
            services.AddFluentValidationAutoValidation();
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly()); // validasyonları ekliyoruz
            services.AddAutoMapper(Assembly.GetExecutingAssembly()); // mapping profillerini ekliyoruz
            return services;
        }

    }
}
