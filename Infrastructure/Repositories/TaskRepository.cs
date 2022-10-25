using Application.Tasks.Repositories;
using Infrastructure.Context;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class TaskRepository : GenericRepository<Domain.Poco.Task>, ITaskRepository
    {
        public TaskRepository(MeamaContext dbContext) : base(dbContext)
        {
        }
    }
}
