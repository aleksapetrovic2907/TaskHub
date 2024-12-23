using TaskStatus = TaskHub.Domain.Enums.TaskStatus;

namespace TaskHub.Domain.Entities
{
    public class Task
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        public DateTime? DueAt { get; set; }
        public TaskStatus Status { get; set; }

        public User User { get; set; }
        public Guid UserId { get; set; }
    }
}
