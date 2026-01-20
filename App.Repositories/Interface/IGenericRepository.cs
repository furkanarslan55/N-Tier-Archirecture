using System.Linq.Expressions;

namespace App.Repositories.Interface
{
    public interface IGenericRepository<T> where T : class
    {


        IQueryable<T> GetAll();

        IQueryable<T> Where(Expression<Func<T, bool >> predicate);
        ValueTask<T?> GetByIdAsync(int id);
        ValueTask AddAsync(T entity);
        Task<T> UpdateAsync(T entity);
        Task<T> DeleteAsync(T entity);






    }
}
