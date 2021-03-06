﻿using Geek.Project.Infrastructure.QueryModel;

namespace Geek.Project.Core.ViewModel.SysUser
{
    public class UserParameters : QueryParameters<string>
    {
        public string UserName { get; set; }

        public string RealName { get; set; }

        public int? Status { get; set; }

        public string CreateTime { get; set; }
    }
}
