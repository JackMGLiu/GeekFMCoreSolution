using Geek.Project.Entity.Base;

namespace Geek.Project.Entity
{
    public class SysRole : IEntity
    {
        public int Id { get; set; }

        public string RoleName { get; set; }
    }
}
