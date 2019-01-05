using Geek.Project.Core.ViewModel.SysRole;
using System;

namespace Geek.Project.Core.ViewModel.SysUser
{
    public class UserViewModel
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        //public string Password { get; set; }
        public string RealName { get; set; }
        public string RoleId { get; set; }
        public int? Status { get; set; }
        public int? Age { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public DateTime? UpdateTime { get; set; }
        public DateTime? CreateTime { get; set; }
        public string Remark { get; set; }

        public RoleViewModel Role { get; set; }
    }
}
