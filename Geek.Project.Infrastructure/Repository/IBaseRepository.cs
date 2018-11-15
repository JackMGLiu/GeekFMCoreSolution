using Geek.Project.Entity.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Geek.Project.Infrastructure.Repository
{
    public interface IBaseRepository<TEntity, TKey>
         where TEntity : class, IEntity
    {
        #region Query

        TEntity GetByKey(TKey key);
        Task<TEntity> GetByKeyAsync(TKey key);

        TEntity GetSingle(Expression<Func<TEntity, bool>> expression, params string[] includes);

        Task<TEntity> GetSingleAsync(Expression<Func<TEntity, bool>> expression, params string[] includes);

        bool IsExist(Expression<Func<TEntity, bool>> expression, params string[] includes);

        Task<bool> IsExistAsync(Expression<Func<TEntity, bool>> expression, params string[] includes);

        IQueryable<TEntity> Query();

        IQueryable<TEntity> Query(params string[] includes);

        IQueryable<TEntity> Query(Expression<Func<TEntity, bool>> expression);

        IQueryable<TEntity> Query(Expression<Func<TEntity, bool>> expression, params string[] includes);

        #endregion

        #region Insert

        void Insert(TEntity entity);
        void Insert(IEnumerable<TEntity> entities);
        Task InsertAsync(TEntity entity);
        Task InsertAsync(IEnumerable<TEntity> entities);

        #endregion

        #region Update

        void Update(TEntity entity);

        void Update(IEnumerable<TEntity> entities);

        #endregion

        #region Remove

        void Remove(TKey key);

        void Remove(TEntity entity);

        void Remove(Expression<Func<TEntity, bool>> expression);

        #endregion
    }
}
