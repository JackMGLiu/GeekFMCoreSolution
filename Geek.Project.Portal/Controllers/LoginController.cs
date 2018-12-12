using Geek.Project.Core.Service.Interface;
using Geek.Project.Core.ViewModel.SysUser;
using Geek.Project.Portal.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

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
            if (data.Item1)
            {
                var claimIdentity = new ClaimsIdentity(AdminAuthorizeAttribute.AdminAuthenticationScheme);
                claimIdentity.AddClaim(new Claim("userId", data.Item3.Id.ToString()));
                claimIdentity.AddClaim(new Claim("userName", data.Item3.UserName.ToString()));
                claimIdentity.AddClaim(new Claim("roleId", data.Item3.RoleId.ToString()));
                var claimsPrincipal = new ClaimsPrincipal(claimIdentity);
                await HttpContext.SignInAsync(claimsPrincipal, new AuthenticationProperties
                {
                    ExpiresUtc = DateTime.Now.AddHours(1),//有效时间1小时
                    IsPersistent = true,
                    AllowRefresh = false,
                });
                var res = new
                {
                    status = data.Item1,
                    msg = data.Item2,
                    data = data.Item3
                };
                return Json(res);
                //var user = new ClaimsPrincipal(new ClaimsIdentity(new[]
                //    {
                //        new Claim("userId",data.Item3.Id.ToString()),
                //        new Claim("userName",data.Item3.UserName.ToString()),
                //        new Claim("roleId",data.Item3.RoleId.ToString())
                //    }, AdminAuthorizeAttribute.AdminAuthenticationScheme));
            }
            else
            {
                var res = new
                {
                    status = data.Item1,
                    msg = data.Item2,
                    data = data.Item3
                };
                return Json(res);
            }
        }
    }
}
