using AutoMapper;
using Geek.Project.Core.Service.Interface;
using Geek.Project.Core.ViewModel.SysLog;
using Geek.Project.Infrastructure.QueryModel;
using Geek.Project.Portal.Controllers;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Geek.Project.Portal.Areas.System.Controllers
{
    [Area("System")]
    public class SysLogController : BaseController
    {
        private readonly IMapper _mapper;
        private readonly ISysLogService _sysLogService;

        public SysLogController(IMapper mapper, ISysLogService sysLogService)
        {
            this._mapper = mapper;
            this._sysLogService = sysLogService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> PageLogData(LogParameters parameters)
        {
            var data = await _sysLogService.GetAllLogsAsync(parameters);
            var res = _mapper.Map<IEnumerable<SysLogViewModel>>(data);
            var shapedLogResources = res.ToDynamicIEnumerable(parameters.Fields);

            var jsonRes = new
            {
                code = 0,
                message = "",
                count = data.TotalItemsCount,
                data = shapedLogResources
            };
            LogInformation("查询日志信息列表");
            return Json(jsonRes);
        }

    }
}
