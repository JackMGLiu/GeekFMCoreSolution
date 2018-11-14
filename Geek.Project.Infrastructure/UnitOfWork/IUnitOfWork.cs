using Geek.Project.Infrastructure.DataBase;
using System;
using System.Threading.Tasks;

namespace Geek.Project.Infrastructure.UnitOfWork
{
    /// <summary>
    /// 工作单元接口
    /// </summary>
    public interface IUnitOfWork : IDisposable
    {
        ProjectDbContext CurrentDbContext { get; }

        int Commit();

        Task<int> CommitAsync();
    }
}
