using Geek.Project.Entity.Base;
using Geek.Project.Infrastructure.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Geek.Project.Infrastructure.Repository
{
    public class BaseRepository<TEntity, TKey> : IBaseRepository<TEntity, TKey>
        where TEntity : class, IEntity
    {
        #region fields

        private readonly DbSet<TEntity> _dbSet;
        private readonly DbContext _dbContext;

        #endregion

        public BaseRepository(IUnitOfWork unitOfWork)
        {
            _dbContext = unitOfWork.CurrentDbContext;
            _dbSet = _dbContext.Set<TEntity>();
        }

        #region query

        public TEntity GetByKey(TKey key)
        {
            return _dbSet.Find(key);
        }

        public async Task<TEntity> GetByKeyAsync(TKey key)
        {
            return await _dbSet.FindAsync(key);
        }

        public TEntity GetSingle(Expression<Func<TEntity, bool>> expression, params string[] includes)
        {
            if (includes.Length > 0)
            {
                IQueryable<TEntity> data = _dbSet.AsQueryable<TEntity>();
                foreach (var prop in includes)
                {
                    data = data.Include(prop);
                }
                return data.Single(expression);
            }
            else
            {
                return _dbSet.Single(expression); ;
            }
        }

        public async Task<TEntity> GetSingleAsync(Expression<Func<TEntity, bool>> expression, params string[] includes)
        {
            if (includes.Length > 0)
            {
                IQueryable<TEntity> data = _dbSet.AsQueryable<TEntity>();
                foreach (var prop in includes)
                {
                    data = data.Include(prop);
                }
                return await data.SingleAsync(expression);
            }
            else
            {
                return await _dbSet.SingleAsync(expression);
            }
        }

        public bool IsExist(Expression<Func<TEntity, bool>> expression, params string[] includes)
        {
            if (includes.Length > 0)
            {
                IQueryable<TEntity> data = _dbSet.AsQueryable<TEntity>();
                foreach (var prop in includes)
                {
                    data = data.Include(prop);
                }
                return data.Any(expression);
            }
            else
            {
                return _dbSet.Any(expression);
            }
        }

        public async Task<bool> IsExistAsync(Expression<Func<TEntity, bool>> expression, params string[] includes)
        {
            if (includes.Length > 0)
            {
                IQueryable<TEntity> data = _dbSet.AsQueryable<TEntity>();
                foreach (var prop in includes)
                {
                    data = data.Include(prop);
                }
                return await data.AnyAsync(expression);
            }
            else
            {
                return await _dbSet.AnyAsync(expression);
            }
        }

        public IQueryable<TEntity> Query()
        {
            return _dbSet.AsQueryable<TEntity>();
        }

        public IQueryable<TEntity> Query(params string[] includes)
        {
            if (includes.Length > 0)
            {
                IQueryable<TEntity> data = _dbSet.AsQueryable<TEntity>();
                foreach (var prop in includes)
                {
                    data = data.Include(prop);
                }
                return data;
            }
            else
            {
                return _dbSet.AsQueryable<TEntity>();
            }
        }

        public IQueryable<TEntity> Query(Expression<Func<TEntity, bool>> expression)
        {
            return _dbSet.Where(expression);
        }

        public IQueryable<TEntity> Query(Expression<Func<TEntity, bool>> expression, params string[] includes)
        {
            if (includes.Length > 0)
            {
                IQueryable<TEntity> data = _dbSet.AsQueryable<TEntity>();
                foreach (var prop in includes)
                {
                    data = data.Include(prop);
                }
                return data.Where(expression); ;
            }
            else
            {
                return _dbSet.Where(expression);
            }
        }

        #endregion

        #region insert

        public void Insert(TEntity entity)
        {
            _dbSet.Add(entity);
        }

        public void Insert(IEnumerable<TEntity> entities)
        {
            _dbSet.AddRange(entities);
        }

        public async Task InsertAsync(TEntity entity)
        {
            await _dbSet.AddAsync(entity);
        }

        public async Task InsertAsync(IEnumerable<TEntity> entities)
        {
            await _dbSet.AddRangeAsync(entities);
        }

        #endregion

        #region remove

        public void Remove(TKey key)
        {
            var entity = _dbSet.Find(key);
            _dbSet.Remove(entity);
        }

        public void Remove(TEntity entity)
        {
            _dbSet.Remove(entity);
        }

        public void Remove(Expression<Func<TEntity, bool>> expression)
        {
            var entities = _dbSet.AsNoTracking().Where(expression).ToList();
            _dbSet.RemoveRange(entities);
        }

        #endregion

        #region update

        public void Update(TEntity entity)
        {
            _dbSet.Update(entity);
        }

        public void Update(IEnumerable<TEntity> entities)
        {
            _dbSet.UpdateRange(entities);
        }

        #endregion
    }
}
