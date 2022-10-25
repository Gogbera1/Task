using Application.Roles.Request;
using Application.Roles.Service;
using Common.Enums;
using MeamaTask.Auth;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace MeamaTask.Controllers
{
    [Route("api/Role")]
    public class RoleController : BaseController
    {
        private readonly IRoleService _service;
        public RoleController(IRoleService service)
        {
            _service = service;
        }

        [HttpPost("Role")]
        public async Task<IActionResult> Create(RoleRequestModel request)
        {
            var result = await _service.CreateRole(request);
            if (result.Status != ResultCodeEnum.Code200Success)
                return Error(result.Status);
            return Ok(result.Status);
        }

        [HttpGet("Roles")]
        public async Task<IActionResult> Get([FromQuery] string[] roleNames)
        {
            var result = await _service.GetRoles(roleNames);
            if (result.Status != ResultCodeEnum.Code200Success)
                return Error(result.Status);
            return Ok(result.Value);
        }

        [HttpPost("SetPermissions")]
        public async Task<IActionResult> Create(SetRolePermissionRequestModel model)
        {
            var result = await _service.SetPermissionToRole(model);
            if (result.Status != ResultCodeEnum.Code200Success)
                return Error(result.Status);
            return Ok(result.Status);
        }
    }
}
