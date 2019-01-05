using Geek.Project.Core.Repository.Interface;
using Geek.Project.Entity;
using Geek.Project.Infrastructure.Repository;
using Geek.Project.Infrastructure.UnitOfWork;

namespace Geek.Project.Core.Repository.Impl
{
    public class SysRoleRepository : BaseRepository<SysRole, string>, ISysRoleRepository
    {
        public SysRoleRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}
