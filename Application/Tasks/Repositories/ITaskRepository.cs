using Common.Repository;

namespace Application.Tasks.Repositories
{
    public interface ITaskRepository : IGenericRepository<Domain.Poco.Task>
    {
    }
}
