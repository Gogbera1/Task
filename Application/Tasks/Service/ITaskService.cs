using Application.Tasks.Request;
using Common.Enums;
using Common.Resultwrapper;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Tasks.Service
{
    public interface ITaskService
    {
        ResultWrapper<List<TaskModel>> GetTasks(CancellationToken cancellationToken);
        Task<ResultWrapper<TaskModel>> GetTaskById(int id, CancellationToken cancellationToken);
        Task<ResultWrapper<ResultCodeEnum>> DeleteTask(int id, CancellationToken cancellationToken);
        Task<ResultWrapper<ResultCodeEnum>> CreateTask(TaskModel request, CancellationToken cancellationToken);
        Task<ResultWrapper<ResultCodeEnum>> UpdateTask(int id, TaskModel request, CancellationToken cancellationToken);
    }
}
