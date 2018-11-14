using Microsoft.Extensions.Logging;
using System;
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
