using Application.Tasks.Request;
using Application.Tasks.Service;
using Common.Enums;
using MeamaTask.Auth;
using Microsoft.AspNetCore.Mvc;
using System.Threading;
using System.Threading.Tasks;

namespace MeamaTask.Controllers
{
    [Route("api/Task")]
    public class TaskController : BaseController
    {
        /* გამარჯობა გიორგი რეალურად 3 დღეში დავწერე და ისე ვერ დავალაგე ყველაფერი როგორც მინდოდა ბევრად უკედაც შემიძლია...
         * ვერ მოვასწარი სამწუხაროდ ფაილების აფლოადი მარა გაკეთებული მაქ ეგ ზოგადად დაჟე პედეეფის გენერაციებიც მაქ გაკეთებული უფასო ვარინტს ვგულისხმობ(ქასთომი)
         * იდეალურად ვერ გადავტესტე ყველაფერი მაგრამ მთელი სულით და გულით გავაკეთე :))) */
        private readonly ITaskService _service;
        public TaskController(ITaskService service) => _service = service;

        [HttpPost("Create")]
        [PermissionAuthorizeAttribute("Create")]
        public async Task<IActionResult> Create(TaskModel request, CancellationToken cancellationToken)
        {
            var result = await _service.CreateTask(request, cancellationToken);
            if (result.Status != ResultCodeEnum.Code200Success)
                return Error(result.Status);
            return Ok();
        }

        [HttpGet("Tasks")]
        [PermissionAuthorizeAttribute("Read")]
        public async Task<IActionResult> Get(CancellationToken cancellationToken)
        {
            var result = _service.GetTasks(cancellationToken);
            if (result.Status != ResultCodeEnum.Code200Success)
                return Error(result.Status);
            return Ok(result.Value);
        }

        [HttpGet("{id}")]
        [PermissionAuthorizeAttribute("Read")]
        public async Task<IActionResult> Get(int id, CancellationToken cancellationToken)
        {
            var result = await _service.GetTaskById(id, cancellationToken);
            if (result.Status != ResultCodeEnum.Code200Success)
                return Error(result.Status);
            return Ok(result.Value);
        }

        [HttpPut("{id}")]
        [PermissionAuthorizeAttribute("Update")]
        public async Task<IActionResult> Update(int id, [FromBody] TaskModel model, CancellationToken cancellationToken)
        {
            var result = await _service.UpdateTask(id, model,cancellationToken);
            if (result.Status != ResultCodeEnum.Code200Success)
                return Error(result.Status);
            return Ok(result.Status);
        }

        [HttpDelete("{id}")]
        [PermissionAuthorizeAttribute("Delete")]
        public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
        {
            var result = await _service.DeleteTask(id, cancellationToken);
            if (result.Status != ResultCodeEnum.Code200Success)
                return Error(result.Status);
            return Ok(result.Status);
        }
    }
}
