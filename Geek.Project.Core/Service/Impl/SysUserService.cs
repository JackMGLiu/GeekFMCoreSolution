using Geek.Project.Core.Repository.Interface;
using Geek.Project.Core.Service.Interface;
using Geek.Project.Entity;
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

        public SysUserService(IUnitOfWork uow, ISysUserRepository userRepository)
        {
            this._uow = uow;
            this._userRepository = userRepository;
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

        public void Update()
        {
            //_uow.BeginTransaction();
            var user = _userRepository.GetByKey(625);
            user.UpdateTime = DateTime.Now;
            user.Remark = "Hello World122122";
            _userRepository.Update(user);
            _uow.Commit();
        }
    }
}
