using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TaskHub.Domain.Entities;
using Task = TaskHub.Domain.Entities.Task;

namespace TaskHub.Infrastructure.Contexts
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : IdentityDbContext<User, IdentityRole<Guid>, Guid>(options)
    {
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // User entity configuration (optional, already managed by Identity)
            modelBuilder.Entity<User>()
                .HasMany(u => u.Tasks)
                .WithOne(t => t.User)
                .HasForeignKey(t => t.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            // Task entity configuration
            modelBuilder.Entity<Task>()
                .HasKey(t => t.Id); // Primary key

            modelBuilder.Entity<Task>()
                .Property(t => t.Status)
                .IsRequired();

            base.OnModelCreating(modelBuilder); // Always call the base method
        }

        public DbSet<Task> Tasks { get; set; }
    }
}
