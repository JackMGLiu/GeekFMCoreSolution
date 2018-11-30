using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Geek.IdentityServer.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace Geek.IdentityServer.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Login(string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model, string returnUrl = null)
        {
            if (ModelState.IsValid)
            {
                ViewData["ReturnUrl"] = returnUrl;
                if (model == null)
                {
                    ModelState.AddModelError(nameof(model.UserName), "当前用户名不存在");
                }
                else
                {
                    if (model.UserName == "admin" && model.Password == "admin123")
                    {
                        //（保存）认证信息字典
                        AuthenticationProperties props = new AuthenticationProperties
                        {
                            IsPersistent = true,    //认证信息是否跨域有效
                            ExpiresUtc = DateTimeOffset.UtcNow.Add(TimeSpan.FromMinutes(30))    //凭据有效时间
                        };

                        //await Microsoft.AspNetCore.Http.AuthenticationManagerExtensions.SignInAsync(
                        //    HttpContext, user.SubjectId, user.Username, props);
                        await Microsoft.AspNetCore.Http.AuthenticationManagerExtensions.SignInAsync(HttpContext, "1000000", model.UserName, props);

                        if (Url.IsLocalUrl(returnUrl))
                        {
                            return Redirect(returnUrl);
                        }
                    }
                    ModelState.AddModelError(nameof(model.Password), "账号或密码错误");
                }
            }
            return View(model);
        }
    }
}