using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace WebApp.Data.Abstract
{
    public interface IBaseRepository<T> where T : class, new()
    {
        T Get(Expression<Func<T, bool>> filter);
        IList<T> GetList(Expression<Func<T, bool>> filter = null);
        bool Add(T entity);
        bool Update(T entity);
        bool Delete(T entity);

        Task<ICollection<T>> GetListAsync(Expression<Func<T, bool>> filter = null);

        Task<T> GetAsync(Expression<Func<T, bool>> filter);
    }
}