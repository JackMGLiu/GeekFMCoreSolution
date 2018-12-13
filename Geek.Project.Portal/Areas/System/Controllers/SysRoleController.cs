using Geek.Project.Core.Service.Interface;
using Geek.Project.Portal.Controllers;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace Geek.Project.Portal.Areas.System.Controllers
{
    [Area("System")]
    public class SysRoleController : BaseController
    {
        private readonly ISysRoleService _sysRoleService;

        public SysRoleController(ISysRoleService sysRoleService)
        {
            this._sysRoleService = sysRoleService;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> GetRoles()
        {
            var roles = await _sysRoleService.GetRoleListAsync();
            var res = new
            {
                code = "0",
                msg = "",
                data = roles.Select(r => new
                {
                    value = r.Id,
                    name = r.RoleName,
                    selected = "",
                    disabled = ""
                })
            };
            return Json(res);
        }
    }
}
