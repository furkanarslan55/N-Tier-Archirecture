using App.Repositories.Data;
using App.Repositories.Interface;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace App.Repositories.Concrete.EntityFramework
{
    public class EfGenericRepository<T>(AppDbContext context) : IGenericRepository<T> where T : class
    {
        protected AppDbContext Context = context;

        private readonly DbSet<T> _dbSet = context.Set<T>();

       public IQueryable<T> GetAll() => _dbSet.AsQueryable();

        public IQueryable<T> Where(Expression<Func<T, bool>> predicate) => _dbSet.Where(predicate);
     
        public  ValueTask<T?> GetByIdAsync(int id) => _dbSet.FindAsync(id);

        public async ValueTask AddAsync(T entity) => _dbSet.AddAsync(entity);

        public Task<T> UpdateAsync(T entity)
        {
            _dbSet.Update(entity);
            return Task.FromResult(entity);
        }

        public Task<T> DeleteAsync(T entity)
        {
            _dbSet.Remove(entity);
            return Task.FromResult(entity);
        }



    }
}
