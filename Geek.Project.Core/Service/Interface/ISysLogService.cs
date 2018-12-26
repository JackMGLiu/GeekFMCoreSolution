using Geek.Project.Core.ViewModel.SysLog;
using Geek.Project.Entity;
using Geek.Project.Infrastructure.QueryModel;
using System.Threading.Tasks;

namespace Geek.Project.Core.Service.Interface
{
    public interface ISysLogService
    {
        Task<PagedList<SysLog>> GetAllLogsAsync(LogParameters parameters);
    }
}
