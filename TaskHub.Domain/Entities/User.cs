using Microsoft.AspNetCore.Identity;

namespace TaskHub.Domain.Entities
{
    public class User : IdentityUser<Guid>
    {
        public string Name { get; set; }
        public ICollection<Task> Tasks { get; set; }
    }
}
