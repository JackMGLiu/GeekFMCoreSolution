using Geek.Project.Entity;
using Geek.Project.Infrastructure.Repository;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Geek.Project.Core.Repository.Interface
{
    public interface ISysUserRepository : IBaseRepository<SysUser, int>
    {
        Task<List<SysUser>> GetAllListAsync();
    }
}
