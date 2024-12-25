namespace TaskHub.Presentation.Models
{
    public class CreateTaskDto
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime? DueAt { get; set; }
    }
}
