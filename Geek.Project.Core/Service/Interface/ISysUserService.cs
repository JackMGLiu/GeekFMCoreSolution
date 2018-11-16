using Geek.Project.Core.ViewModel.SysUser;
using Geek.Project.Entity;
using Geek.Project.Infrastructure.QueryModel;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Geek.Project.Core.Service.Interface
{
    public interface ISysUserService
    {
        Task<bool> AddUser(CreateUserModel model);

        List<SysUser> GetUserList();

        SysUser GetUserByKey(int key);

        Task<SysUser> GetUserByKeyAsync(int key);
        bool Update(SysUser model);

        Task<List<SysUser>> GetUserListAsync();

        Task<List<SysUser>> GetUsersAsync();

        Task<bool> IsExist();

        Task<bool> IsExist(string userName);

        Task<PagedList<SysUser>> GetAllUsersAsync(UserParameters parameters);

        /// <summary>
        /// 用户登录
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<Tuple<bool, string, UserViewModel>> AccountLogin(LoginViewModel model);
    }
}
