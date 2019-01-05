using Geek.Project.Infrastructure.QueryModel;

namespace Geek.Project.Core.ViewModel.SysLog
{
    public class LogParameters : QueryParameters<int>
    {
        public string Level { get; set; }

        public string TimeStamp { get; set; }
    }
}
