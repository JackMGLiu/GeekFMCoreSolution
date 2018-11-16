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

        public void Update()
        {
            //_uow.BeginTransaction();
            var user = _userRepository.GetByKey(625);
            user.UpdateTime = DateTime.Now;
            user.Remark = "Hello World122122";
            _userRepository.Update(user);
            _uow.Commit();
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


    }
}
