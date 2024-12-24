using TaskEntity = TaskHub.Domain.Entities.Task;

namespace TaskHub.Application.Services
{
    public interface ITaskService
    {
        Task<IEnumerable<TaskEntity>> GetAllTasksAsync(Guid userId);
        Task<TaskEntity> GetTaskByIdAsync(Guid userId, Guid taskId);
        Task<TaskEntity> CreateTaskAsync(Guid userId, TaskEntity task);
        Task<TaskEntity> UpdateTaskAsync(Guid userId, Guid taskId, TaskEntity task);
        Task DeleteTaskAsync(Guid userId, Guid taskId);
    }
}
