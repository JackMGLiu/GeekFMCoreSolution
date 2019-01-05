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

        SysUser GetUserByKey(string key);

        Task<SysUser> GetUserByKeyAsync(string key);
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

        /// <summary>
        /// 修改用户状态
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        Task UpdateStatus(string userId, int status);

        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        Task<bool> DeleteUser(string userId);

        /// <summary>
        /// 删除用户集合
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        Task<bool> DeleteUsers(string[] arrIds);
    }
}
