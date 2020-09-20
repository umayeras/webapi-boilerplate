using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using WebApp.Core.Extensions;

namespace WebApp.Data.Repositories
{
    public class BaseRepository<TEntity> : IBaseRepository<TEntity>
        where TEntity : class
    {
        #region ctor

        private readonly DbContext context;
        private readonly DbSet<TEntity> dbSet;
        private readonly ILogger<BaseRepository<TEntity>> logger;

        public BaseRepository(DbContext context, ILogger<BaseRepository<TEntity>> logger)
        {
            this.context = context ?? throw new ArgumentException(nameof(context));
            this.logger = logger;
            
            dbSet = context.Set<TEntity>();
        }

        #endregion

        public async Task<IEnumerable<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> filter = null)
        {
            return filter == null
                ? await dbSet.AsNoTracking().ToListAsync()
                : await dbSet.Where(filter).AsNoTracking().ToListAsync();
        }

        public async Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> filter)
        {
            return await dbSet.SingleOrDefaultAsync(filter);
        }

        public async Task<bool> AddAsync(TEntity entity)
        {
            try
            {
                await dbSet.AddAsync(entity);
                return true;
            }
            catch (Exception ex)
            {
                var message = GetLogMessage(entity, nameof(AddAsync), ex.Message);
                logger.LogError(message);
                return false;
            }
        }

        public bool Add(TEntity entity)
        {
            try
            {
                dbSet.Add(entity);
                return true;
            }
            catch (Exception ex)
            {
                var message = GetLogMessage(entity, nameof(Add), ex.Message);
                logger.LogError(message);
                
                return false;
            }
        }

        public bool Update(TEntity entity)
        {
            try
            {
                dbSet.Attach(entity); 
                context.Entry(entity).State = EntityState.Modified;
                
                return true;
            }
            catch (Exception ex)
            {
                var message = GetLogMessage(entity, nameof(Update), ex.Message);
                logger.LogError(message);
                return false;
            }
        }

        public bool Delete(TEntity entity)
        {
            try
            {
                dbSet.Attach(entity); 
                context.Entry(entity).State = EntityState.Modified;
                
                return true;
            }
            catch (Exception ex)
            {
                var message = GetLogMessage(entity, nameof(Delete), ex.Message);
                logger.LogError(message);

                return false;
            }
        }

        public bool DeleteItem(TEntity entity)
        {
            try
            {
                dbSet.Remove(entity);
                return true;
            }
            catch (Exception ex)
            {
                var message = GetLogMessage(entity, nameof(DeleteItem), ex.Message);
                logger.LogError(message);

                return false;
            }
        }

        private static string GetLogMessage(TEntity entity, string methodName, string message)
        {
            return entity.GetType().CreateLogMessage(methodName, message);
        }
    }
}