using Geek.Project.Core.Service.Interface;
using Geek.Project.Core.ViewModel.SysUser;
using Geek.Project.Infrastructure.QueryModel;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

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

        [HttpGet()]
        public async Task<IActionResult> Test4()
        {
            var res = await _sysUserService.IsExist();
            return Ok(res);
        }

        [HttpGet]
        public IActionResult Test7()
        {
            try
            {
                Convert.ToInt32("abc");
                return View();
            }
            catch (Exception ex)
            {
                throw new Exception("不能转换");
            }
        }

        public IActionResult Test6()
        {
            return View();
        }

        [HttpGet()]
        public async Task<IActionResult> Test5(UserParameters parameters)
        {
            var data = await _sysUserService.GetAllUsersAsync(parameters);
            //var res = _mapper.Map<IEnumerable<UserViewModel>>(data);
            var shapedUserResources = data.ToDynamicIEnumerable(parameters.Fields);

            //var previousPageLink =
            //    data.HasPrevious ? CreatePostUri(parameters, PaginationResourceUriType.PreviousPage) : null;

            //var nextPageLink = data.HasNext ? CreatePostUri(parameters, PaginationResourceUriType.NextPage) : null;

            var meta = new
            {
                data.PageIndex,
                data.PageSize,
                TotalCount = data.TotalItemsCount,
                data.PageCount,
                resources = shapedUserResources
                //previousPageLink,
                //nextPageLink
            };
            return Ok(meta);
        }
    }
}
