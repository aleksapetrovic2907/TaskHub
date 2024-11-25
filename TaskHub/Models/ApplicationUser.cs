using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace TaskHub.Models
{
    public class ApplicationUser : IdentityUser
    {
        [Required]
        [StringLength(255, ErrorMessage = "The first name must contain a maximum of 255 characters.")]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }
        [Required]
        [StringLength(255, ErrorMessage = "The last name must contain a maximum of 255 characters.")]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }
        public string FullName => $"{LastName}, {FirstName}";
    }
}
