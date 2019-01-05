using Geek.Project.Entity.Base;

namespace Geek.Project.Entity
{
    /// <summary>
    /// 角色权限信息
    /// </summary>
    public class RolePermission : IEntity<int>
    {
        /// <summary>
        /// 主键
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 角色主键
        /// </summary>
        public string RoleId { get; set; }

        /// <summary>
        /// 菜单主键
        /// </summary>
        public int MenuId { get; set; }

        /// <summary>
        /// 操作类型（功能权限）
        /// </summary>
        public string Permission { get; set; }
    }
}
