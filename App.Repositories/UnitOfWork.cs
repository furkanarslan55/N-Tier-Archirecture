using App.Repositories.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Repositories
{
    public class UnitOfWork(AppDbContext context) : IUnitOfWork  //primary constructor
    {
        public  Task<int> SaveChangesAsync()
        {
            return  context.SaveChangesAsync();
        }
    


    }
}
