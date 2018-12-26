using Geek.Project.Core.ViewModel;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Linq;

namespace Geek.Project.Portal.Models
{
    /// <summary>
    /// 用户登录验证
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = true, AllowMultiple = true)]
    public class AdminAuthorizeAttribute : AuthorizeAttribute, IAuthorizationFilter
    {
        public const string AdminAuthenticationScheme = "AdminAuthenticationScheme";//自定义一个默认的登录方案
        public AdminAuthorizeAttribute()
        {
            this.AuthenticationSchemes = AdminAuthenticationScheme;
        }


        public void OnAuthorization(AuthorizationFilterContext context)
        {
            //获取登录方案
            var authenticate = context.HttpContext.AuthenticateAsync(AdminAuthorizeAttribute.AdminAuthenticationScheme);
            if (authenticate.Result.Succeeded || this.SkipAuthorize(context.ActionDescriptor))
            {
                return;
            }
            HttpRequest httpRequest = context.HttpContext.Request;
            if (httpRequest.IsAjaxRequest())//ajax请求
            {
                ResultModel result = new ResultModel();
                result.status = "redirect";//需要重定向
                result.msg = "登录超时";
                result.data = "";
                context.Result = new JsonResult(result);
            }
            else
            {
                RedirectResult redirectResult = new RedirectResult("~/Login/Index");
                context.Result = redirectResult;
            }

            return;
        }

        protected virtual bool SkipAuthorize(ActionDescriptor actionDescriptor)
        {
            return actionDescriptor.FilterDescriptors
                .Where(a => a.Filter is SkipAuthorizeAttribute).Any();
        }
    }
}
