using Geek.Project.Infrastructure.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Geek.Project.Infrastructure.DataBase
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ProjectDbContext _dbContext;

        #region ctor

        public UnitOfWork(ProjectDbContext dbContext)
        {
            this._dbContext = dbContext;
        }

        #endregion

        public ProjectDbContext CurrentDbContext => _dbContext;

        public int Commit()
        {
            int result = 0;
            try
            {
                result = _dbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                result = -1;
                CleanChanges(_dbContext);
                throw new Exception($"Commit 异常：{ex.InnerException}/r{ ex.Message}");
            }
            return result;
        }

        public async Task<int> CommitAsync()
        {
            int result = 0;
            try
            {
                result = await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                result = -1;
                CleanChanges(_dbContext);
                throw new Exception($"Commit 异常：{ex.InnerException}/r{ ex.Message}");
            }
            return await Task.FromResult(result);
        }

        #region private

        /// <summary>
        /// 操作失败，还原跟踪状态
        /// </summary>
        /// <param name="context"></param>
        private static void CleanChanges(ProjectDbContext context)
        {
            var entries = context.ChangeTracker.Entries().ToArray();
            for (int i = 0; i < entries.Length; i++)
            {
                entries[i].State = EntityState.Detached;
            }
        }

        #endregion

        #region override

        public void Dispose()
        {
            _dbContext.Dispose();
            GC.SuppressFinalize(this);
        }

        #endregion
    }
}
