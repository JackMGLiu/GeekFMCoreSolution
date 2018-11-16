namespace Geek.Project.Core.ViewModel
{
    /// <summary>
    /// 信息返回提示
    /// </summary>
    public class ResultModel
    {
        public ResultModel()
        {
            data = null;
            backurl = "";
        }

        public string status { get; set; }
        public string msg { get; set; }
        public string backurl { get; set; }
        public object data { get; set; }
    }
}
