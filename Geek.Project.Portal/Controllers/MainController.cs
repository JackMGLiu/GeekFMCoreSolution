using Geek.Project.Portal.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Geek.Project.Portal.Controllers
{
    public class MainController : BaseController
    {
        /// <summary>
        /// 首页
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Index()
        {
            var userName = CurrentUser.UserName;
            ViewData["UserName"] = userName;
            return View();
        }

        /// <summary>
        /// 主题设置
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Theme()
        {
            return View();
        }

        /// <summary>
        /// 首页展示
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Console()
        {
            return View();
        }
        public async Task<IActionResult> LogOut(string returnurl)
        {
            try
            {
                await HttpContext.SignOutAsync(AdminAuthorizeAttribute.AdminAuthenticationScheme);
                var res = new
                {
                    status = true,
                    msg = "退出成功",
                    backurl = "/Login/Index"
                };
                return Json(res);
            }
            catch (Exception ex)
            {
                var res = new
                {
                    status = true,
                    msg = "退出失败，请重试！",
                };
                return Json(res);
            }
        }

        /// <summary>
        /// 个人中心
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult CurrentUserInfo()
        {
            return View();
        }
    }
}
