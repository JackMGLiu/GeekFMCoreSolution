using Geek.Project.Entity.Base;
using System;

namespace Geek.Project.Entity
{
    /// <summary>
    /// 系统目录信息
    /// </summary>
    public class SysMenu : IEntity<string>
    {
        /// <summary>
        /// 主键
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// 父节点编号
        /// </summary>
        public int? ParentId { get; set; }

        /// <summary>
        /// 目录名称
        /// </summary>
        public string MenuName { get; set; }

        /// <summary>
        /// 显示名称
        /// </summary>
        public string DisplayName { get; set; }

        /// <summary>
        /// 图标
        /// </summary>
        public string Icon { get; set; }

        /// <summary>
        /// 链接地址
        /// </summary>
        public string LinkUrl { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        public int? SortCode { get; set; }

        /// <summary>
        /// 操作权限（按钮权限时使用）
        /// </summary>
        public string Permission { get; set; }

        /// <summary>
        /// 是否显示
        /// </summary>
        public bool? IsDisplay { get; set; }

        /// <summary>
        /// 是否系统默认
        /// </summary>
        public bool IsDefault { get; set; }

        /// <summary>
        /// 用户状态1.启用；0：禁用
        /// </summary>
        public int? Status { get; set; }

        /// <summary>
        /// 是否删除1.删除；0：未删除
        /// </summary>
        public int? IsDelete { get; set; }

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
