using Geek.Project.Infrastructure.QueryModel;

namespace Geek.Project.Core.ViewModel.SysLog
{
    public class LogParameters : QueryParameters
    {
        public string Level { get; set; }

        public string TimeStamp { get; set; }
    }
}
