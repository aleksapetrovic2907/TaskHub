using Microsoft.EntityFrameworkCore;
using TaskHub.Domain.Entities;
using Task = TaskHub.Domain.Entities.Task;

namespace TaskHub.Infrastructure.Contexts
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Task> Tasks { get; set; }
    }
}
