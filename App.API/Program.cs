using App.Repositories.Data;
using App.Repositories.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using App.Services.Extensions;
using Microsoft.Extensions.Options;
using App.Services;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.Configure<ConnectionStringOption>(
    builder.Configuration.GetSection(ConnectionStringOption.Key));
builder.Services.AddControllers(Options =>
{ 
    
    
    Options.SuppressImplicitRequiredAttributeForNonNullableReferenceTypes = true; // string gibi non-nullable referans tipleri için implicit required attribute'u devre dýþý býrakýr
    Options.Filters.Add<FluentValidationFilter>();
    
    
    
    });

builder.Services
    .AddRepositories(builder.Configuration)
    .AddServices(builder.Configuration);





// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddRepositories(builder.Configuration).AddServices(builder.Configuration);








var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
