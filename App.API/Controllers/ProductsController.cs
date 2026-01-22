using App.Services.Products;
using App.Services.Products.Create;
using App.Services.Products.Update;
using Microsoft.AspNetCore.Mvc;

namespace App.API.Controllers
{

    public class ProductsController(IProductServices productServices) : CustomBaseController
    {


        private readonly IProductServices _productServices = productServices;



        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var serviceresult = await _productServices.GetAllList();


            return CreateActionResult(serviceresult);
        }
        [HttpGet("{pageNumber:int}/{pageSize:int}")] //constraint ekledik int olmalı diye
        public async Task<IActionResult> GetPagedAll(int pageNumber ,int pageSize)
        {
            var serviceresult = await _productServices.GetPagedAllList(pageNumber ,pageSize);


            return CreateActionResult(serviceresult);
        }

        [HttpGet("{id}")]  // bu datayı artık query stringde değil route üzerinden alacağız.

        public async Task<IActionResult> GetById(int id)
        {
            var serviceresult = await _productServices.GetByIdAsync(id);


            return CreateActionResult(serviceresult);



        }

       







        [HttpPost]
        public async Task<IActionResult> Create(CreateProductRequest request)
        {
            var serviceresult = await _productServices.CreateProductAsync(request);
            return CreateActionResult(serviceresult);

        }
        [HttpPut("{id:int}")]

        public async Task<IActionResult> Update( int id,UpdateProductRequest request)
        {
            var serviceresult = await _productServices.UpdateProductAsync(id,request);
            return CreateActionResult(serviceresult);
        }



        [HttpPatch("stock")] //parçalı güncelleme 
        public async Task<IActionResult> UpdateStock(int id, int quantity)
        {
            var serviceresult = await _productServices.UpdateStockAsync(id, quantity);
            return CreateActionResult(serviceresult);
        }








        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var serviceresult = await _productServices.DeleteProductAsync(id);
            return CreateActionResult(serviceresult);
        }
    }
}