using Geek.Project.Core.Repository.Interface;
using Geek.Project.Core.Service.Interface;
using Geek.Project.Core.ViewModel.SysUser;
using Geek.Project.Entity;
using Geek.Project.Infrastructure.Extensions;
using Geek.Project.Infrastructure.QueryModel;
using Geek.Project.Infrastructure.Services;
using Geek.Project.Infrastructure.UnitOfWork;
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
        private readonly ISysUserRepository _userRepository;
        private readonly IPropertyMappingContainer _propertyMappingContainer;

        public SysUserService(IUnitOfWork uow, ISysUserRepository userRepository, IPropertyMappingContainer propertyMappingContainer)
        {
            this._uow = uow;
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
            //if (!string.IsNullOrEmpty(parameters.Title))
            //{
            //    var title = parameters.Title.ToLowerInvariant();
            //    query = query.Where(x => x.Title.ToLowerInvariant() == title);
            //}

            //var query = _dbContext.Posts.OrderBy(x => x.Id);
            //query = query.OrderBy(x => x.Id);

            query = query.ApplySort(parameters.OrderBy, _propertyMappingContainer.Resolve<UserViewModel, SysUser>()); //排序

            var count = await query.CountAsync();
            var data = await query
                .Skip(parameters.PageIndex * parameters.PageSize)
                .Take(parameters.PageSize)
                .ToListAsync();
            return new PagedList<SysUser>(parameters.PageIndex, parameters.PageSize, count, data);
        }

    }
}
