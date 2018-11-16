using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Geek.Project.Core.Service.Interface;
using Geek.Project.Core.ViewModel;
using Geek.Project.Core.ViewModel.SysUser;
using Geek.Project.Entity;
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

        /// <summary>
        /// 用户表单
        /// </summary>
        /// <returns></returns>
        [HttpGet("sys/userform")]
        public IActionResult Form()
        {
            return View();
        }

        [HttpPost("sys/userform")]
        public async Task<IActionResult> Form(CreateUserModel model)
        {
            var jsonResult = new ResultModel();
            var res = await _sysUserService.AddUser(model);
            if (res)
            {
                jsonResult.status = "1";
                jsonResult.msg = "新增信息成功";
            }
            else
            {
                jsonResult.status = "0";
                jsonResult.msg = "新增信息失败";
            }
            return Json(jsonResult);
        }

        [HttpGet("sysuser/checkval")]
        public async Task<IActionResult> Check(string userName)
        {
            var res = await _sysUserService.IsExist(userName);
            return Json(res);
        }
    }
}