using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebApp.Data.Abstract;
using WebApp.Framework.Abstract;

namespace WebApp.Data.Repositories
{
    public class BaseRepository<TEntity, TContext> : IBaseRepository<TEntity>
        where TEntity : class, new()
        where TContext : DbContext, new()
    {
        #region ctor

        private readonly ILoggingService logger;

        protected BaseRepository(ILoggingService logger)
        {
            this.logger = logger;
        }

        #endregion

        #region get / get async

        public IList<TEntity> GetList(Expression<Func<TEntity, bool>> filter = null)
        {
            using var context = new TContext();

            return filter == null
                ? context.Set<TEntity>().ToList()
                : context.Set<TEntity>().Where(filter).ToList();
        }

        public TEntity Get(Expression<Func<TEntity, bool>> filter)
        {
            using var context = new TContext();

            return context.Set<TEntity>().SingleOrDefault(filter);
        }

        public async Task<ICollection<TEntity>> GetListAsync(Expression<Func<TEntity, bool>> filter = null)
        {
            await using var context = new TContext();

            return filter == null
                ? await context.Set<TEntity>().ToListAsync()
                : await context.Set<TEntity>().Where(filter).ToListAsync();
        }

        public async Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> filter)
        {
            await using var context = new TContext();

            return await context.Set<TEntity>().SingleOrDefaultAsync(filter);
        }

        #endregion

        public bool Add(TEntity entity)
        {
            try
            {
                using var context = new TContext();
                var addedEntity = context.Entry(entity);
                addedEntity.State = EntityState.Added;

                context.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                logger.LogError(
                    SetMethodNameForLogMessage(nameof(Add)),
                    $"{entity.GetType().Name} {nameof(Add)} failed. ResultMessage: {ex.Message}");
                return false;
            }
        }

        public bool Update(TEntity entity)
        {
            try
            {
                using var context = new TContext();
                var updatedEntity = context.Entry(entity);
                updatedEntity.State = EntityState.Modified;

                context.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                logger.LogError(
                    SetMethodNameForLogMessage(nameof(Update)),
                    $"{entity.GetType().Name} {nameof(Update)} failed. ResultMessage: {ex.Message}");
                return false;
            }
        }

        public bool Delete(TEntity entity)
        {
            try
            {
                using var context = new TContext();
                var deletedEntity = context.Entry(entity);
                deletedEntity.State = EntityState.Modified;

                context.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                logger.LogError(
                    SetMethodNameForLogMessage(nameof(Delete)),
                    $"{entity.GetType().Name} {nameof(Delete)} failed. ResultMessage: {ex.Message}");
                return false;
            }
        }

        private string SetMethodNameForLogMessage(string methodName)
        {
            return $"{GetType().Name}.{methodName}";
        }
    }
}