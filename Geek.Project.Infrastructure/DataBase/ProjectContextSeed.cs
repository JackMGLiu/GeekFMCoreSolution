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
                        UserName = "admin",
                        Password = "admin123".Md5Hash(),
                        RealName = "刘健",
                        Status = 1,
                        Age = 33,
                        Email = "125267283@qq.com",
                        CreateTime = DateTime.Now,
                        Remark = "管理员账户"

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
