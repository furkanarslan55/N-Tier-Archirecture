using App.Repositories.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Services.Products
{
    public record ProductDto( int Id, string Name, decimal Price, int Stock , int CategoryId);
    // burada record kulllanımı daha hızlı veriye ulaşmak ve immutable (değiştirilemez) veri yapıları oluşturmak için tercih edilmiştir.
    // primary constructor kullanımı ile tüm özellikler tek satırda tanımlanmıştır.


}
