using Geek.Project.Entity;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Geek.Project.Core.Service.Interface
{
    public interface ISysRoleService
    {
        Task<List<SysRole>> GetRoleListAsync();
    }
}
