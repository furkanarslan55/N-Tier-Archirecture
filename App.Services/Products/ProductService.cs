using App.Repositories;
using App.Repositories.Entities;
using App.Repositories.Interface;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace App.Services.Products
{
    public class ProductService(IProducRepository producRepository,IUnitOfWork unitOfWork) : IProductServices
    {



        public async Task<ServiceResult <List<ProductDto>>> GetTopPriceProductsAsync(int count)
        {
            var products = await producRepository.GetTopPriceProductsAsync(count);
            var ProductDto = products.Select(p => new ProductDto(p.Id, p.Name, p.Price, p.Stock)).ToList();

            return new  ServiceResult <List<ProductDto >> ()
            {
                Data = ProductDto,
              
            };
        }

        public async Task<ServiceResult<List<ProductDto>>> GetAllList()
                    {
            var products = await producRepository.GetAll().ToListAsync();
            var ProductDto = products.Select(p => new ProductDto(p.Id, p.Name, p.Price, p.Stock)).ToList();
            return  ServiceResult<List<ProductDto>>.Success(ProductDto);
           
        }
        public async Task<ServiceResult<ProductDto>>GetByIdAsync( int id)

        {
            var product = await producRepository.GetByIdAsync(id);

            if( product is null)
            {


                ServiceResult<ProductDto>.Fail("Product not found",HttpStatusCode.NotFound);

            }
            var ProductDto = new ProductDto(product.Id, product.Name, product.Price, product.Stock);

            return ServiceResult<ProductDto>.Success(ProductDto!);
        }

        public async Task<ServiceResult<CreateProductResponse>> CreateProductAsync(CreateProductRequest request)
        {
            var newProduct = new Product
            {
                Name = request.Name,
                Price = request.Price,
                Stock = request.Stock
            };
            await producRepository.AddAsync(newProduct);
            await unitOfWork.SaveChangesAsync();
            var response = new CreateProductResponse(newProduct.Id);
            return ServiceResult<CreateProductResponse>.Success(response);
        }

      public async Task<ServiceResult> UpdateProductAsync (int id,UpdateProductRequest request)
        {


            var product = await producRepository.GetByIdAsync(id);

            if (product is null)
            {
                return ServiceResult.Fail("Product not found", HttpStatusCode.NotFound);
            }
            product.Name = request.Name;
            product.Price = request.Price;
            product.Stock = request.Stock;

            producRepository.UpdateAsync(product);
            await unitOfWork.SaveChangesAsync();
            return ServiceResult.Success(HttpStatusCode.NoContent); // no content dönmemizin sebebi güncelleme ve silmede geriye sadece status codu dönmek yeterlidir.
        }

        public async Task<ServiceResult> DeleteProductAsync(int id)
        {
            var product = await producRepository.GetByIdAsync(id);
            if (product is null)
            {
                return ServiceResult.Fail("Product not found", HttpStatusCode.NotFound);
            }
            producRepository.DeleteAsync(product);
            await unitOfWork.SaveChangesAsync();
            return ServiceResult.Success();
        }
    }
}
