using App.Repositories;
using App.Repositories.Entities;
using App.Repositories.Interface;
using App.Services.Products.Create;
using App.Services.Products.Update;
using AutoMapper;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace App.Services.Products
{
    public class ProductService(
        IProducRepository producRepository,
        IUnitOfWork unitOfWork,
        IValidator<CreateProductRequest> createProductRequestValidator,IMapper mapper) : IProductServices
    {



        public async Task<ServiceResult <List<ProductDto>>> GetTopPriceProductsAsync(int count)
        {
            var products = await producRepository.GetTopPriceProductsAsync(count);
    

            var ProductDto = mapper.Map<List<ProductDto>>(products);

            return new  ServiceResult <List<ProductDto >> ()
            {
                Data = ProductDto,
              
            };
        }

        public async Task<ServiceResult<List<ProductDto>>> GetAllList()
                    {
            var products = await producRepository.GetAll().ToListAsync();
        var productAsDto = mapper.Map<List<ProductDto>>(products);
            return  ServiceResult<List<ProductDto>>.Success(productAsDto);
           
        }

        public async Task<ServiceResult<List<ProductDto>>> GetPagedAllList(int pageNumber,int pageSize )
        {
           
            var products =await producRepository.GetAll()
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            var ProductDto = products.Select(p => new ProductDto(p.Id, p.Name, p.Price, p.Stock)).ToList();
            return ServiceResult<List<ProductDto>>.Success(ProductDto);


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

            var anyProduct = await producRepository.Where(x => x.Name == request.Name).AnyAsync();
            if(anyProduct)
            {
                return ServiceResult<CreateProductResponse>.Fail("Ürün ismi mevcuttur.", HttpStatusCode.BadRequest);
            }






         var product = mapper.Map<Product>(request);

            await producRepository.AddAsync(product);
            await unitOfWork.SaveChangesAsync();
            var response = new CreateProductResponse(product.Id);
            return ServiceResult<CreateProductResponse>.SuccessAsCreated(response,$"api/products/{product.Id}");
        }

      public async Task<ServiceResult> UpdateProductAsync (int id,UpdateProductRequest request)
        {


            var product = await producRepository.GetByIdAsync(id);

            if (product is null)
            {
                return ServiceResult.Fail("Product not found", HttpStatusCode.NotFound);
            }
            var iSProductNameExist = await producRepository.Where(x => x.Name == request.Name && x.Id !=product.Id).AnyAsync(); //güncelleme yaparken ürün ismi kendi ismiyle çakışmasın diye böyle bir kontrol yapıyoruz.
            if (iSProductNameExist)
            {
                return ServiceResult.Fail("Ürün ismi mevcuttur.", HttpStatusCode.BadRequest);
            }



            //product.Name = request.Name;
            //product.Price = request.Price;
            //product.Stock = request.Stock;

            product =mapper.Map(request,product); // AutoMapper ile de yapabiliriz.

            producRepository.UpdateAsync(product);
            await unitOfWork.SaveChangesAsync();
            return ServiceResult.Success(HttpStatusCode.NoContent); // no content dönmemizin sebebi güncelleme ve silmede geriye sadece status codu dönmek yeterlidir.
        }

        public async Task<ServiceResult> UpdateStockAsync (int productId,int quantity)
        {
            var product = await producRepository.GetByIdAsync(productId);
            if (product is null)
            {
                return ServiceResult.Fail("Product not found", HttpStatusCode.NotFound);
            }
            product.Stock = quantity;
            producRepository.UpdateAsync(product);
            await unitOfWork.SaveChangesAsync();
            return ServiceResult.Success( HttpStatusCode.NoContent); // no content dönmemizin sebebi güncelleme ve silmede geriye sadece status codu dönmek yeterlidir.
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
