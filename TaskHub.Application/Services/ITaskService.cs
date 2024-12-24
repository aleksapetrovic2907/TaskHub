namespace TaskHub.Application.Services
{
    public interface ITaskService
    {
        Task<IEnumerable<Domain.Entities.Task>> GetAllTasksAsync();
        Task<Domain.Entities.Task> GetTaskByIdAsync(Guid userId, Guid taskId);
        Task<Domain.Entities.Task> CreateTaskAsync(Guid userId, Domain.Entities.Task task);
        Task<Domain.Entities.Task> UpdateTaskAsync(Guid userId, Guid taskId, Domain.Entities.Task task);
        Task DeleteTaskAsync(Guid userId, Guid taskId);
    }
}
