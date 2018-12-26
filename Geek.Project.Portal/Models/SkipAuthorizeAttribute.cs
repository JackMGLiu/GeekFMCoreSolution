using Microsoft.AspNetCore.Mvc.Filters;
using System;

namespace Geek.Project.Portal.Models
{
    /// <summary>
    /// 跳过属性检查
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    public class SkipAuthorizeAttribute : Attribute, IFilterMetadata
    {

    }
}
