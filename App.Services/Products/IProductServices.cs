using App.Repositories.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Services.Products
{
    public interface IProductServices
    {
        Task<ServiceResult<List<Product>>> GetTopPriceProductsAsync(int count);

        Task<ServiceResult<Product>>GetProductByIdAsync(int id);
    }
}
