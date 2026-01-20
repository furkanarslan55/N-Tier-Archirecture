using App.Repositories.Entities;
using App.Repositories.Interface;
using System.Net;

namespace App.Services.Products
{
    public class ProductService(IProducRepository producRepository) : IProductServices
    {



        public async Task<ServiceResult <List<Product>>> GetTopPriceProductsAsync(int count)
        {
            var products = await producRepository.GetTopPriceProductsAsync(count);

            return new  ServiceResult <List<Product >> ()
            {
                Data = products,
              
            };
        }

        public async Task<ServiceResult<Product>>GetProductByIdAsync( int id)

        {
            var product = await producRepository.GetByIdAsync(id);

            if( product is null)
            {


                ServiceResult<Product>.Fail("Product not found",HttpStatusCode.NotFound);

            }


           return ServiceResult<Product>.Success(product!);
        }



    }
}
