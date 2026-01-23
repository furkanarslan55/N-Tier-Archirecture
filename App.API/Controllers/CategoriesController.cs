using App.Services.Categories;
using App.Services.Categories.Create;
using App.Services.Categories.Update;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace App.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController(ICategoryServices categoryServices) : CustomBaseController
    {

        [HttpGet]

        public async Task<IActionResult> GetCategories()
        {
            return CreateActionResult(await categoryServices.GetAllListAsync());
        }


        [HttpGet("products")]

        public async Task<IActionResult> GetCategoriesWithProducts()
        {
            return CreateActionResult(await categoryServices.GetCategoryWithProductsAsync());
        }




        [HttpGet("{id}")]
        public async Task<IActionResult> GetCategoryById(int id)
        {
            return CreateActionResult(await categoryServices.GetByIdAsync(id));
        }

        [HttpPost]
        public async Task<IActionResult> CreateCategory(CreateCategoryRequest request)
        {
            return CreateActionResult(await categoryServices.Create(request));

           
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            return CreateActionResult(await categoryServices.DeleteAsync(id));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCategory(int id,UpdateCategoryRequest request)
        {
            return CreateActionResult(await categoryServices.UpdateAsync(id,request));
        }

        


    }
}
