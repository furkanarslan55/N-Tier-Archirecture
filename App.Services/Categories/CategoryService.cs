using App.Repositories;
using App.Repositories.Entities;
using App.Repositories.Interface;
using App.Services.Categories.Create;
using App.Services.Categories.Update;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Services.Categories
{
    public class CategoryService(ICategoryRepository categoryRepository,IUnitOfWork unitOfWork,IMapper mapper) :ICategoryServices
    {
        public async Task<ServiceResult<CategoryWithProductsDto>> GetCategoryWithProducts(int categoryId)
        {
            var categories = await categoryRepository.GetCategoryWithProductsAsync(categoryId);
            if(categories == null )
            {
                return ServiceResult<CategoryWithProductsDto>.Fail("kategori bulunamadı", System.Net.HttpStatusCode.NotFound);
            }


          
            var categoryDtos = mapper.Map<CategoryWithProductsDto>(categories);
            return ServiceResult<CategoryWithProductsDto>.Success(categoryDtos);
        }
        public async Task<ServiceResult<List<CategoryWithProductsDto>>> GetCategoryWithProductsAsync()
        {
            var categories = await categoryRepository.GetCategoryWithProducts().ToListAsync();
          


            var categoryDtos = mapper.Map<List<CategoryWithProductsDto>>(categories);
            return ServiceResult<List<CategoryWithProductsDto>>.Success(categoryDtos);
        }








        public async Task<ServiceResult<List<CategoryDto>>> GetAllListAsync()
        {
            var categories = await categoryRepository.GetAll().ToListAsync();
            var categoryDtos = mapper.Map<List<CategoryDto>>(categories);
            return ServiceResult<List<CategoryDto>>.Success(categoryDtos);
        }

        public async Task<ServiceResult<CategoryDto>> GetByIdAsync(int id)
        {
            var category = await categoryRepository.GetByIdAsync(id);
            if (category == null)
            {
                return ServiceResult<CategoryDto>.Fail("kategori bulunamadı", System.Net.HttpStatusCode.NotFound);
            }
            var categoryDto = mapper.Map<CategoryDto>(category);
            return ServiceResult<CategoryDto>.Success(categoryDto);
        }

        public  async Task<ServiceResult<int>> Create(CreateCategoryRequest request)
        {

            var anyCategory = await categoryRepository.Where(x => x.Name == request.Name).AnyAsync();
            if (anyCategory)
            {
                return ServiceResult<int>.Fail("kategori ismi veritabında bulunmaktadır", System.Net.HttpStatusCode.NotFound);
            }

            var newCategory = new Category { Name = request.Name };
            await categoryRepository.AddAsync(newCategory);
        
            await unitOfWork.SaveChangesAsync();

            return ServiceResult<int>.Success(newCategory.Id);


        }

        public async Task<ServiceResult> UpdateAsync(int id, UpdateCategoryRequest request)
        {



            var category = await categoryRepository.GetByIdAsync(id);

            if (category == null)
            {
                return ServiceResult.Fail("kategori bulunamadı", System.Net.HttpStatusCode.NotFound);
            }

            var anyCategory = await categoryRepository.Where(x => x.Name == request.Name && x.Id != id).AnyAsync();
            if (anyCategory)
            {
                return ServiceResult.Fail("kategori ismi veritabında bulunmaktadır", System.Net.HttpStatusCode.NotFound);
            }


            category =mapper.Map(request, category);
            categoryRepository.UpdateAsync(category);
            await unitOfWork.SaveChangesAsync();
            return ServiceResult.Success(System.Net.HttpStatusCode.NoContent);

        }

        public async Task<ServiceResult> DeleteAsync(int id)
        {
            var category = await categoryRepository.GetByIdAsync(id);
            if (category == null)
            {
                return ServiceResult.Fail("kategori bulunamadı", System.Net.HttpStatusCode.NotFound);
            }
            categoryRepository.DeleteAsync(category);
            await unitOfWork.SaveChangesAsync();
            return ServiceResult.Success(System.Net.HttpStatusCode.NoContent);
        }

        
    }
}
