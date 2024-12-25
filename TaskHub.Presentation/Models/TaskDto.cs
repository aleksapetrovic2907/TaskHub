using TaskStatus = TaskHub.Presentation.Enums.TaskStatus;

namespace TaskHub.Presentation.Models
{
    public class TaskDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? DueAt { get; set; }
        public TaskStatus Status { get; set; }
    }
}