using AutoMapper;
using Geek.Project.Core.Repository.Interface;
using Geek.Project.Core.Service.Interface;
using Geek.Project.Core.ViewModel.SysUser;
using Geek.Project.Entity;
using Geek.Project.Infrastructure.Extensions;
using Geek.Project.Infrastructure.QueryModel;
using Geek.Project.Infrastructure.Services;
using Geek.Project.Infrastructure.UnitOfWork;
using Geek.Project.Utils.Extensions;
using Geek.Project.Utils.Security;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Geek.Project.Core.Service.Impl
{
    public class SysUserService : ISysUserService
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        private readonly ISysUserRepository _userRepository;
        private readonly IPropertyMappingContainer _propertyMappingContainer;

        public SysUserService(IUnitOfWork uow, IMapper mapper, ISysUserRepository userRepository, IPropertyMappingContainer propertyMappingContainer)
        {
            this._uow = uow;
            this._mapper = mapper;
            this._userRepository = userRepository;
            this._propertyMappingContainer = propertyMappingContainer;
        }

        public SysUser GetUserByKey(int key)
        {
            return _userRepository.GetByKey(key);
        }

        public List<SysUser> GetUserList()
        {
            return _userRepository.Query().ToList();
        }

        public async Task<List<SysUser>> GetUserListAsync()
        {
            //return await _userRepository.Query().ToListAsync();
            return await _userRepository.GetAllListAsync();
        }

        public async Task<List<SysUser>> GetUsersAsync()
        {
            return await _userRepository.Query(u => u.Age >= 23, "Role").ToListAsync();
        }

        public async Task<bool> IsExist()
        {
            return await _userRepository.IsExistAsync(u => u.Age == 63);
        }

        public async Task<bool> IsExist(string userName)
        {
            return await _userRepository.IsExistAsync(u => u.UserName == userName);
        }

        public bool Update(SysUser model)
        {
            model.UpdateTime = DateTime.Now;
            _userRepository.Update(model);
            var res = _uow.Commit();
            return res > 0;
        }

        public async Task<PagedList<SysUser>> GetAllUsersAsync(UserParameters parameters)
        {
            var query = _userRepository.Query("Role");
            if (!string.IsNullOrEmpty(parameters.UserName))
            {
                var userName = parameters.UserName.ToLowerInvariant();
                query = query.Where(x => x.UserName.ToLowerInvariant().Contains(userName));
            }
            if (!string.IsNullOrEmpty(parameters.RealName))
            {
                var realName = parameters.RealName.ToLowerInvariant();
                query = query.Where(x => x.RealName.ToLowerInvariant().Contains(realName));
            }
            if (parameters.Status.HasValue)
            {
                var status = parameters.Status.Value;
                query = query.Where(x => x.Status == status);
            }
            if (!string.IsNullOrEmpty(parameters.CreateTime))
            {
                var time = parameters.CreateTime.Trim().Split('-');
                var start = DateTime.Parse(time[0] + '-' + time[1] + '-' + time[2]);
                var end = DateTime.Parse(time[3] + '-' + time[4] + '-' + time[5]);
                query = query.Where(x => x.CreateTime.Value >= start && x.CreateTime.Value <= end);
            }
            //var query = _dbContext.Posts.OrderBy(x => x.Id);
            //query = query.OrderBy(x => x.Id);

            query = query.ApplySort(parameters.OrderBy, _propertyMappingContainer.Resolve<UserViewModel, SysUser>()); //排序

            var count = await query.CountAsync();
            var data = await query
                .Skip((parameters.PageIndex - 1) * parameters.PageSize)
                .Take(parameters.PageIndex * parameters.PageSize)
                .ToListAsync();
            return new PagedList<SysUser>(parameters.PageIndex, parameters.PageSize, count, data);
        }

        public async Task<Tuple<bool, string, UserViewModel>> AccountLogin(LoginViewModel model)
        {
            Tuple<bool, string, UserViewModel> res = null;
            if (!model.IsEmpty())
            {
                if (!model.LoginName.IsEmpty())
                {
                    var currentAcc = await _userRepository.GetSingleAsync(m => m.UserName == model.LoginName);
                    if (!currentAcc.IsEmpty())
                    {
                        if (currentAcc.Password == model.LoginPass.Md5Hash())
                        {
                            var user = _mapper.Map<UserViewModel>(currentAcc);
                            res = Tuple.Create<bool, string, UserViewModel>(true, "登录成功", user);
                        }
                        else
                        {
                            res = Tuple.Create<bool, string, UserViewModel>(false, "账号或密码错误，请重试", null);
                        }
                    }
                    else
                    {
                        res = Tuple.Create<bool, string, UserViewModel>(false, "当前用户不存在", null);
                    }
                }
            }
            else
            {
                res = Tuple.Create<bool, string, UserViewModel>(false, "请输入登录信息", null);
            }
            return res;
        }

        public async Task<bool> AddUser(CreateUserModel model)
        {
            if (!model.IsEmpty())
            {
                var user = _mapper.Map<SysUser>(model);
                await _userRepository.InsertAsync(user);
                var res = await _uow.CommitAsync();
                return res > 0;
            }
            return false;
        }

        public async Task<SysUser> GetUserByKeyAsync(int key)
        {
            return await _userRepository.GetSingleAsync(u => u.Id == key, "Role");
        }

        public async Task UpdateStatus(int userId, int status)
        {
            var user = await _userRepository.GetSingleAsync(u => u.Id == userId);
            if (!user.IsEmpty())
            {
                user.Status = status;
                _userRepository.Update(user);
                await _uow.CommitAsync();
            }
        }

        public async Task<bool> DeleteUser(int userId)
        {
            _userRepository.Remove(userId);
            return await _uow.CommitAsync() > 0;
        }

        public async Task<bool> DeleteUsers(int[] arrIds)
        {
            if (arrIds.Length > 0)
            {
                _userRepository.Remove(u => arrIds.Contains(u.Id));
                return await _uow.CommitAsync() > 0;
            }
            return false;
        }
    }
}
