using App.Services.Categories.Create;
using App.Services.Categories.Update;

namespace App.Services.Categories
{
    public interface ICategoryServices
    {
        Task<ServiceResult<List<CategoryDto>>> GetAllListAsync();
        Task<ServiceResult<CategoryDto>> GetByIdAsync(int id);
        Task<ServiceResult<CategoryWithProductsDto>> GetCategoryWithProducts(int categoryId);
        Task<ServiceResult<List<CategoryWithProductsDto>>> GetCategoryWithProductsAsync();

       Task<ServiceResult<int>> Create(CreateCategoryRequest request);
        Task<ServiceResult> UpdateAsync(int id, UpdateCategoryRequest request);
        Task<ServiceResult> DeleteAsync(int id);
        
    }
}
