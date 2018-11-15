using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Geek.Project.Core.Service.Interface;
using Geek.Project.Core.ViewModel.SysUser;
using Geek.Project.Infrastructure.QueryModel;
using Microsoft.AspNetCore.Mvc;

namespace Geek.Project.Portal.Areas.System.Controllers
{
    [Area("System")]
    public class SysUserController : Controller
    {
        private readonly IMapper _mapper;
        private readonly ISysUserService _sysUserService;

        public SysUserController(IMapper mapper, ISysUserService sysUserService)
        {
            this._mapper = mapper;
            this._sysUserService = sysUserService;
        }

        /// <summary>
        /// 用户列表
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("sys/users")]
        public async Task<IActionResult> PageUserData(UserParameters parameters)
        {
            var data = await _sysUserService.GetAllUsersAsync(parameters);
            var res = _mapper.Map<IEnumerable<UserViewModel>>(data);
            var shapedUserResources = res.ToDynamicIEnumerable(parameters.Fields);

            //var jsonRes = new
            //{
            //    data.PageIndex,
            //    data.PageSize,
            //    TotalCount = data.TotalItemsCount,
            //    data.PageCount,
            //    resources = shapedUserResources
            //    //previousPageLink,
            //    //nextPageLink
            //};

            var jsonRes = new
            {
                code = 0,
                message = "",
                count = data.TotalItemsCount,
                data = shapedUserResources
            };
            return Json(jsonRes);
        }
    }
}