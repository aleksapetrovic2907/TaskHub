using Microsoft.EntityFrameworkCore;
using TaskHub.Application.Services;
using TaskHub.Infrastructure.Contexts;
using TaskEntity = TaskHub.Domain.Entities.Task;

namespace TaskHub.Infrastructure.Services
{
    public class TaskService : ITaskService
    {
        private readonly ApplicationDbContext _context;

        public TaskService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<TaskEntity>> GetAllTasksAsync(Guid userId)
        {
            return await _context.Tasks
                .Where(t => t.UserId == userId)
                .ToListAsync();
        }

        public async Task<TaskEntity> GetTaskByIdAsync(Guid userId, Guid taskId)
        {
            return await _context.Tasks
                .FirstOrDefaultAsync(t => t.UserId == userId && t.Id == taskId);
        }

        public async Task<TaskEntity> CreateTaskAsync(Guid userId, TaskEntity task)
        {
            task.UserId = userId;
            _context.Tasks.Add(task);
            await _context.SaveChangesAsync();
            return task;
        }

        public async Task<TaskEntity> UpdateTaskAsync(Guid userId, Guid taskId, TaskEntity task)
        {
            var targetTask = await _context.Tasks
                .FirstOrDefaultAsync(t => t.UserId == userId && t.Id == taskId) ?? throw new KeyNotFoundException("Task not found");

            task.Title = targetTask.Title;
            task.Description = targetTask.Description;
            task.Status = targetTask.Status;
            task.DueAt = targetTask.DueAt;

            await _context.SaveChangesAsync();
            return task;
        }

        public async Task DeleteTaskAsync(Guid userId, Guid taskId)
        {
            var targetTask = await _context.Tasks
                .FirstOrDefaultAsync(t => t.UserId == userId && t.Id == taskId) ?? throw new KeyNotFoundException("Task not found");

            _context.Tasks.Remove(targetTask);
            await _context.SaveChangesAsync();
        }
    }
}
