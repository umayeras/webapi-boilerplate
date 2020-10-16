using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace WebApp.Data.Repositories
{
    public interface IBaseRepository<T> where T : class
    {
        Task<IQueryable<T>> GetAllAsync(Expression<Func<T, bool>> filter = null);
        Task<T> GetAsync(Expression<Func<T, bool>> filter);
        Task<bool> AddAsync(T entity);
        bool Add(T entity);
        bool Update(T entity);
        bool Delete(T entity);
        bool DeleteItem(T entity);
    }
}