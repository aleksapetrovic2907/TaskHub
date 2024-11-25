using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace TaskHub.Models
{
    public class UserTask
    {
        [Key]
        public Guid TaskId { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public DateTime CreatedDate { get; set; }
        [ValidateNever]
        public string Status { get; set; }

        public string? AssignedToId { get; set; }
        [ValidateNever]
        public ApplicationUser? AssignedTo { get; set; }
    }
}
