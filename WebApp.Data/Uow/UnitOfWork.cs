using System;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using WebApp.Core.Extensions;
using WebApp.Data.Repositories;

namespace WebApp.Data.Uow
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly WebAppDbContext context;
        private Dictionary<(Type type, string name), object> repositories;
        private readonly ILogger<UnitOfWork> logger;

        public UnitOfWork(WebAppDbContext context, ILogger<UnitOfWork> logger)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
            this.logger = logger;
        }

        public int Save()
        {
            try
            {
                return context.SaveChanges();
            }
            catch (Exception ex)
            {
                var message = GetType().CreateLogMessage(nameof(Save), ex.Message);
                logger.LogError(message);
                throw;
            }
        }

        public IBaseRepository<TEntity> GetRepository<TEntity>() where TEntity : class
        {
            return (IBaseRepository<TEntity>) GetOrAddRepository(typeof(TEntity),
                new BaseRepository<TEntity>(context, new NullLogger<BaseRepository<TEntity>>()));
        }

        private object GetOrAddRepository(Type type, object repo)
        {
            repositories ??= new Dictionary<(Type type, string Name), object>();

            if (repositories.TryGetValue((type, repo.GetType().FullName), out var repository))
            {
                return repository;
            }

            repositories.Add((type, repo.GetType().FullName), repo);
            return repo;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            if (!disposing || context == null)
            {
                return;
            }

            context.Dispose();
        }
    }
}