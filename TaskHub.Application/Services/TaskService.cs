using Microsoft.EntityFrameworkCore;
using TaskHub.Infrastructure.Contexts;

namespace TaskHub.Application.Services
{
    public class TaskService : ITaskService
    {
        private readonly ApplicationDbContext _context;

        public TaskService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Domain.Entities.Task>> GetAllTasksAsync(Guid userId)
        {
            return await _context.Tasks
                .Where(t => t.UserId == userId)
                .ToListAsync();
        }

        public async Task<Domain.Entities.Task> GetTaskByIdAsync(Guid userId, Guid taskId)
        {
            return await _context.Tasks
                .FirstOrDefaultAsync(t => t.UserId == userId && t.Id == taskId);
        }

        public async Task<Domain.Entities.Task> CreateTaskAsync(Guid userId, Domain.Entities.Task task)
        {
            task.UserId = userId;
            _context.Tasks.Add(task);
            await _context.SaveChangesAsync();
            return task;
        }

        public async Task<Domain.Entities.Task> UpdateTaskAsync(Guid userId, Guid taskId, Domain.Entities.Task task)
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
