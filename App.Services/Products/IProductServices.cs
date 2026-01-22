using App.Repositories.Entities;
using App.Services.Products.Create;
using App.Services.Products.Update;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Services.Products
{
    public interface IProductServices
    {
        Task<ServiceResult<List<ProductDto>>> GetTopPriceProductsAsync(int count);
        Task<ServiceResult< List<ProductDto>>> GetAllList();
        Task<ServiceResult<List<ProductDto>>> GetPagedAllList(int pageNumber, int pageSize);
        Task<ServiceResult<ProductDto?>>GetByIdAsync(int id);
        Task<ServiceResult<CreateProductResponse>> CreateProductAsync(CreateProductRequest request);
        Task<ServiceResult> UpdateProductAsync(int id, UpdateProductRequest request);
        Task<ServiceResult> UpdateStockAsync(int productId, int quantity);
        Task<ServiceResult> DeleteProductAsync(int id);
    }
}
