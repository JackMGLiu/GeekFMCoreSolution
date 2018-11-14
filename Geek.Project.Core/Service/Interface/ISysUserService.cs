using Geek.Project.Entity;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Geek.Project.Core.Service.Interface
{
    public interface ISysUserService
    {
        List<SysUser> GetUserList();

        SysUser GetUserByKey(int key);

        void Update();

        Task<List<SysUser>> GetUserListAsync();

        Task<List<SysUser>> GetUsersAsync();
    }
}
