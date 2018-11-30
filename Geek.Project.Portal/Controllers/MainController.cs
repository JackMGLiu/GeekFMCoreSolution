using Geek.Project.Portal.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Geek.Project.Portal.Controllers
{
    [Authorize]
    public class MainController : Controller
    {
        /// <summary>
        /// 首页
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Index()
        {
            //var name = CurrentUser._userViewModel.RealName;
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

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("Index", "Main");
        }
    }
}