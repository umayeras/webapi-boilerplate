using System;
using WebApp.Data.Repositories;

namespace WebApp.Data.Uow
{
    public interface IUnitOfWork : IDisposable
    {
        int Save();
        
        IBaseRepository<TEntity> GetRepository<TEntity>() where TEntity : class;
    }
}