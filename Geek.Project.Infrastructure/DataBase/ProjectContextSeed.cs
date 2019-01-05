using Geek.Project.Entity;
using Geek.Project.Utils.Security;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Geek.Project.Infrastructure.DataBase
{
    public class ProjectContextSeed
    {
        public static async Task SeedAsync(ProjectDbContext context, ILoggerFactory loggerFactory, int retry = 0)
        {
            int retryForAvailability = retry;

            try
            {
                context.Database.Migrate();
                if (!context.SysUsers.Any())
                {
                    context.SysUsers.Add(new SysUser
                    {
                        Id = "100000",
                        RoleId = "System",
                        UserName = "admin",
                        Password = "admin123".Md5Hash(),
                        RealName = "刘健",
                        NickName = "极客锋芒",
                        Status = 1,
                        IsDelete = 0,
                        Mobile = "18636936239",
                        Address = "山西省太原市杏花岭区",
                        Age = 33,
                        Email = "125267283@qq.com",
                        CreateTime = DateTime.Now,
                        Remark = "管理员账户"
                    });
                }

                if (!context.SysRoles.Any())
                {
                    context.SysRoles.Add(new SysRole
                    {
                        Id = "System",
                        RoleName = "超级管理员",
                        IsSuperManager = true,
                        IsDefault = true,
                        Status = 1,
                        IsDelete = 0,
                        CreateTime = DateTime.Now,
                        Remark = "超级管理员"
                    });
                }
                //数据
                await context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                if (retryForAvailability < 10)
                {
                    retryForAvailability++;
                    var logger = loggerFactory.CreateLogger<ProjectContextSeed>();
                    logger.LogError(ex.Message);
                    await SeedAsync(context, loggerFactory, retryForAvailability);
                }
            }
        }
    }
}
