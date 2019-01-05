using Geek.Project.Core.Repository.Interface;
using Geek.Project.Entity;
using Geek.Project.Infrastructure.Repository;
using Geek.Project.Infrastructure.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Geek.Project.Core.Repository.Impl
{
    public class SysUserRepository : BaseRepository<SysUser, string>, ISysUserRepository
    {
        private readonly IUnitOfWork _unitOfWork;

        public SysUserRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }

        public async Task<List<SysUser>> GetAllListAsync()
        {
            var data = await _unitOfWork.CurrentDbContext.SysUsers.Include("Role").Where(u => int.Parse(u.Id) >= 26).ToListAsync();
            return data;
        }
    }
}
