using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Geek.Project.Core.Service.Interface;
using Geek.Project.Core.ViewModel.SysUser;
using Geek.Project.Utils.Helper;
using Microsoft.AspNetCore.Mvc;

namespace Geek.Project.Portal.Controllers
{
    public class LoginController : Controller
    {
        private readonly ISysUserService _sysUserService;

        public LoginController(ISysUserService sysUserService)
        {
            this._sysUserService = sysUserService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CheckLogin(LoginViewModel model)
        {
            var data = await _sysUserService.AccountLogin(model);
            var res = new
            {
                status = data.Item1,
                msg = data.Item2,
                data = data.Item3
            };
            //HttpContext.Session.Set("CurrentUser", ByteConvertHelper.Object2Bytes(data.Item3));
            return Json(res);
        }
    }
}