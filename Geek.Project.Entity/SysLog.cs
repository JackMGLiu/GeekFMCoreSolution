using Geek.Project.Entity.Base;
using System;

namespace Geek.Project.Entity
{
    public class SysLog : IEntity<int>
    {
        public int Id { get; set; }
        public string Message { get; set; }
        public string MessageTemplate { get; set; }
        public string Level { get; set; }
        public DateTime TimeStamp { get; set; }
        public string Exception { get; set; }
        public string Properties { get; set; }
        public string User { get; set; }
        public string Class { get; set; }
        public string Url { get; set; }
    }
}
