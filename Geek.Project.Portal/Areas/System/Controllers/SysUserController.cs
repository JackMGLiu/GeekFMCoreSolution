using AutoMapper;
using Geek.Project.Core.Service.Interface;
using Geek.Project.Core.ViewModel;
using Geek.Project.Core.ViewModel.SysUser;
using Geek.Project.Infrastructure.QueryModel;
using Geek.Project.Portal.Controllers;
using Geek.Project.Utils.Extensions;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Geek.Project.Portal.Areas.System.Controllers
{
    [Area("System")]
    public class SysUserController : BaseController
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

        [HttpGet]
        public async Task<IActionResult> PageUserData(UserParameters parameters)
        {
            var data = await _sysUserService.GetAllUsersAsync(parameters);
            var res = _mapper.Map<IEnumerable<UserViewModel>>(data);
            var shapedUserResources = res.ToDynamicIEnumerable(parameters.Fields);

            var jsonRes = new
            {
                code = 0,
                message = "",
                count = data.TotalItemsCount,
                data = shapedUserResources
            };
            LogInformation("查询用户信息列表");
            return Json(jsonRes);
        }

        /// <summary>
        /// 用户表单
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Form()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Form(string key, CreateUserModel model)
        {
            var jsonResult = new ResultModel();
            if (!key.IsEmpty())
            {
                var current = await _sysUserService.GetUserByKeyAsync(key);
                if (!current.IsEmpty())
                {

                    current.RealName = model.RealName;
                    current.Age = model.Age;
                    current.Email = model.Email;
                    current.Address = model.Address;
                    current.Remark = model.Remark;
                    var res = _sysUserService.Update(current);
                    if (res)
                    {
                        jsonResult.status = "1";
                        jsonResult.msg = "更新信息成功";
                    }
                    else
                    {
                        jsonResult.status = "0";
                        jsonResult.msg = "更新信息失败";
                    }
                }
                else
                {
                    jsonResult.status = "0";
                    jsonResult.msg = "当前用户不存在，更新信息失败";
                }
            }
            else
            {

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
            }


            return Json(jsonResult);
        }

        [HttpGet]
        public async Task<IActionResult> GetModelByKey(string userId)
        {
            var res = await _sysUserService.GetUserByKeyAsync(userId);
            return Json(res);
        }

        [HttpGet]
        public async Task<IActionResult> CheckName(string userName)
        {
            var res = await _sysUserService.IsExist(userName);
            return Json(res);
        }

        [HttpPost]
        public async Task<IActionResult> CheckStatus(string userId, int status)
        {
            var jsonResult = new ResultModel();
            try
            {
                await _sysUserService.UpdateStatus(userId, status);
                jsonResult.status = "1";
                jsonResult.msg = "修改状态成功";
            }
            catch (Exception)
            {
                jsonResult.status = "0";
                jsonResult.msg = "修改状态失败";
            }
            return Json(jsonResult);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteModel(string userId)
        {
            var jsonResult = new ResultModel();
            var res = await _sysUserService.DeleteUser(userId);
            if (res)
            {
                jsonResult.status = "1";
                jsonResult.msg = "删除成功";
            }
            else
            {
                jsonResult.status = "0";
                jsonResult.msg = "删除失败";
            }
            return Json(jsonResult);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteModels(string userIds)
        {
            var jsonResult = new ResultModel();
            if (!userIds.IsEmpty())
            {
                //int[] ids;
                //ids = Array.ConvertAll<string, int>(userIds.Split(','), s => int.Parse(s));
                string[] ids;
                ids = Array.ConvertAll<string, string>(userIds.Split(','), s => s);
                var res = await _sysUserService.DeleteUsers(ids);
                if (res)
                {
                    jsonResult.status = "1";
                    jsonResult.msg = "删除成功";
                }
                else
                {
                    jsonResult.status = "0";
                    jsonResult.msg = "删除失败";
                }
            }
            else
            {
                jsonResult.status = "0";
                jsonResult.msg = "删除失败";
            }
            return Json(jsonResult);
        }
    }
}
