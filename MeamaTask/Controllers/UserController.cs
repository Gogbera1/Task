using Application.User.Request;
using Application.User.Service;
using Common.Enums;
using MeamaTask.Auth;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace MeamaTask.Controllers
{
    [Route("api/User")]
    public class UserController : BaseController
    {
        private readonly IUserService _service;
        public UserController(IUserService service)
        {
            _service = service;
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login(UserLoginModel request)
        {
            var result = await _service.Login(request);
            if (result.Status != ResultCodeEnum.Code200Success)
                return Error(result.Status);
            return Ok(result.Value);
        }

        [HttpPost("User")]
        public async Task<IActionResult> Create(UserRequestModel request)
        {
            var result = await _service.CreateUser(request);
            if (result.Status != ResultCodeEnum.Code200Success)
                return Error(result.Status);
            return Ok(result.Status);
        }

        [HttpGet("Users")]
        public async Task<IActionResult> Get()
        {
            var result = await _service.GetUsers();
            if (result.Status != ResultCodeEnum.Code200Success)
                return Error(result.Status);
            return Ok(result.Value);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            var result = await _service.GetUserById(id);
            if (result.Status != ResultCodeEnum.Code200Success)
                return Error(result.Status);
            return Ok(result.Value);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, [FromBody] UpdateUserRequestModel model)
        {
            var result = await _service.UpdateUser(id, model);
            if (result.Status != ResultCodeEnum.Code200Success)
                return Error(result.Status);
            return Ok(result.Status);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var result = await _service.DeleteUser(id);
            if (result.Status != ResultCodeEnum.Code200Success)
                return Error(result.Status);
            return Ok(result.Status);
        }
    }
}
