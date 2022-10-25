using Application.Tasks.Repositories;
using Application.Tasks.Request;
using Application.Tasks.Service;
using Common.Enums;
using Common.Resultwrapper;
using Mapster;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Tasks.ServiceImpl
{
    public class TaskService : ITaskService
    {
        #region Injection
        private readonly ITaskRepository _taskRepository;
        private readonly UserManager<IdentityUser> _userManager;
        #endregion

        #region Constructor
        public TaskService(ITaskRepository taskRepository, UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
            _taskRepository = taskRepository;
        }
        #endregion

        #region Methods

        public async Task<ResultWrapper<ResultCodeEnum>> CreateTask(TaskModel request, CancellationToken cancellationToken)
        {
            if (request.UserId == null)
                return new ResultWrapper<ResultCodeEnum> { Status = ResultCodeEnum.Code400BadRequest };
            var user = await _userManager.FindByIdAsync(request.UserId);
            if(user == null)
                return new ResultWrapper<ResultCodeEnum> { Status = ResultCodeEnum.Code400BadRequest };
            var entity = new Domain.Poco.Task
            {
                Title = request.Title,
                Description = request.Description,
                ShortDescription = request.ShortDescription,
                UserId = user.Id,
            };
            await _taskRepository.AddAsync(entity, cancellationToken);
            return new ResultWrapper<ResultCodeEnum> { Status = ResultCodeEnum.Code200Success };
        }

        public async Task<ResultWrapper<TaskModel>> GetTaskById(int id, CancellationToken cancellationToken)
        {
            var entity = await _taskRepository.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
            if (entity == null)
                return new ResultWrapper<TaskModel> { Status = ResultCodeEnum.Code404NotFound };
            var response = entity.Adapt<TaskModel>();
            return new ResultWrapper<TaskModel> { Status = ResultCodeEnum.Code200Success, Value = response };
        }

        public ResultWrapper<List<TaskModel>> GetTasks(CancellationToken cancellationToken)
        {
            var entities = _taskRepository.GetAll().ToList();
            if(entities.Count() == 0)
                return new ResultWrapper<List<TaskModel>> { Status = ResultCodeEnum.Code404NotFound };
            var response = entities.Adapt<List<TaskModel>>();
            return new ResultWrapper<List<TaskModel>> { Status = ResultCodeEnum.Code200Success, Value = response };
        }

        public async Task<ResultWrapper<ResultCodeEnum>> UpdateTask(int id,TaskModel request , CancellationToken cancellationToken)
        {
            var entity = await _taskRepository.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
            if (entity == null)
                return new ResultWrapper<ResultCodeEnum> { Status = ResultCodeEnum.Code404NotFound };
            var user = await _userManager.FindByIdAsync(request.UserId);
            if (user == null)
                return new ResultWrapper<ResultCodeEnum> { Status = ResultCodeEnum.Code400BadRequest };
            entity.Title = request.Title;
            entity.UserId = request.UserId;
            entity.Description = request.Description;
            entity.ShortDescription = request.ShortDescription;
            await _taskRepository.UpdateAsync(entity);
            return new ResultWrapper<ResultCodeEnum> { Status = ResultCodeEnum.Code200Success };
        }

        public async Task<ResultWrapper<ResultCodeEnum>> DeleteTask(int id, CancellationToken cancellationToken)
        {
            var entity = await _taskRepository.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
            if (entity == null)
                return new ResultWrapper<ResultCodeEnum> { Status = ResultCodeEnum.Code404NotFound };
            await _taskRepository.SoftDeleteAsync(entity);
            return new ResultWrapper<ResultCodeEnum> { Status = ResultCodeEnum.Code200Success };
        }
        #endregion
    }
}
