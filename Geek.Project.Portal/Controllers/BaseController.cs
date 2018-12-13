using Geek.Project.Core.ViewModel.LoginModel;
using Geek.Project.Portal.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace Geek.Project.Portal.Controllers
{
    [AdminAuthorize]
    public class BaseController : Controller
    {
        private CurrentUserModel _currentUser;

        public CurrentUserModel CurrentUser
        {
            get
            {
                var islogin = HttpContext.User.Identity.IsAuthenticated;
                if (islogin)
                {
                    _currentUser = new CurrentUserModel();
                    _currentUser.UserId = HttpContext.User.Claims.SingleOrDefault(t => t.Type == "userId").Value;
                    _currentUser.UserName = HttpContext.User.Claims.SingleOrDefault(t => t.Type == "userName").Value;
                    _currentUser.RealName = HttpContext.User.Claims.SingleOrDefault(t => t.Type == "realName").Value;
                    _currentUser.RoleId = HttpContext.User.Claims.SingleOrDefault(t => t.Type == "roleId").Value;
                    return _currentUser;
                }
                else
                {
                    HttpContext.SignOutAsync(AdminAuthorizeAttribute.AdminAuthenticationScheme).Wait();
                    return null;
                }
            }
        }
    }
}
