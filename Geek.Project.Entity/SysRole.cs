using Geek.Project.Entity.Base;
using System;

namespace Geek.Project.Entity
{
    /// <summary>
    /// 系统角色信息
    /// </summary>
    public class SysRole : IEntity<string>
    {
        /// <summary>
        /// 主键
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// 角色名称
        /// </summary>
        public string RoleName { get; set; }

        /// <summary>
        /// 是否超级管理员
        /// </summary>
        public bool? IsSuperManager { get; set; }

        /// <summary>
        /// 是否系统默认
        /// </summary>
        public bool? IsDefault { get; set; }

        /// <summary>
        /// 用户状态1.启用；0：禁用
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// 是否删除1.删除；0：未删除
        /// </summary>
        public int IsDelete { get; set; }

        /// <summary>
        /// 更新时间
        /// </summary>
        public DateTime? UpdateTime { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 备注信息
        /// </summary>
        public string Remark { get; set; }
    }
}
