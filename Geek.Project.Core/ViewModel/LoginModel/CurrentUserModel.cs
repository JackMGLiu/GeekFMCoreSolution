namespace Geek.Project.Core.ViewModel.LoginModel
{
    public class CurrentUserModel
    {
        /// <summary>
        /// 用户主键
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        /// 用户名称
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 真实姓名
        /// </summary>
        public string RealName { get; set; }

        /// <summary>
        /// 角色编号
        /// </summary>
        public string RoleId { get; set; }
    }
}
