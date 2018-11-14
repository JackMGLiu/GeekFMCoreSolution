using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Geek.Project.Core.Service.Interface;
using Microsoft.AspNetCore.Mvc;

namespace Geek.Project.Portal.Controllers
{
    public class DemoController : Controller
    {
        private readonly ISysUserService _sysUserService;

        public DemoController(ISysUserService sysUserService)
        {
            this._sysUserService = sysUserService;
        }

        [HttpGet()]
        public IActionResult Test1()
        {
            var res = _sysUserService.GetUserList();
            return Ok(res);
        }

        [HttpGet()]
        public async Task<IActionResult> Test2()
        {
            var res = await _sysUserService.GetUserListAsync();
            return Ok(res);
        }

        [HttpGet()]
        public async Task<IActionResult> Test3()
        {
            var res = await _sysUserService.GetUsersAsync();
            return Ok(res);
        }
    }
}