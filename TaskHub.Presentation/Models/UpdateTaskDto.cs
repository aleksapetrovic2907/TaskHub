namespace TaskHub.Presentation.Models
{
    public class UpdateTaskDto
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime? DueAt { get; set; }
        public TaskStatus Status { get; set; }
    }
}
