using Geek.Project.Core.ViewModel.LoginModel;
using Geek.Project.Portal.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using Serilog.Context;
using Serilog.Events;
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

        [NonAction]
        protected void LogByLevel(LogEventLevel level, string msg)
        {
            using (LogContext.PushProperty("Class", GetType().FullName)) // 对应于自定义的字段，对Sql server起作用,IDisposable
            using (LogContext.PushProperty("Url", HttpContext.Request.Path.Value))
            using (LogContext.PushProperty("User", CurrentUser.UserName))
            {
                //Log.Write(level, $"{msg} (by {CurrentUser}, at {DateTime.Now:yyyy-MM-dd HH:mm:ss.FFF})");
                Log.Write(level, $"{msg}");
            }
        }

        [NonAction]
        protected void LogVerbose(string msg)
        {
            LogByLevel(LogEventLevel.Verbose, msg);
        }

        [NonAction]
        protected void LogDebug(string msg)
        {
            LogByLevel(LogEventLevel.Debug, msg);
        }

        [NonAction]
        protected void LogInformation(string msg)
        {
            LogByLevel(LogEventLevel.Information, msg);
        }

        [NonAction]
        protected void LogWarning(string msg)
        {
            LogByLevel(LogEventLevel.Warning, msg);
        }

        [NonAction]
        protected void LogError(string msg)
        {
            LogByLevel(LogEventLevel.Error, msg);
        }

        [NonAction]
        protected void LogFatal(string msg)
        {
            LogByLevel(LogEventLevel.Fatal, msg);
        }
    }
}
