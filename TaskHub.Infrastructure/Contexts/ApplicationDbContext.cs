using Microsoft.EntityFrameworkCore;
using TaskHub.Domain.Entities;
using Task = TaskHub.Domain.Entities.Task;

namespace TaskHub.Infrastructure.Contexts
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
    {
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // User entity configuration
            modelBuilder.Entity<User>()
                .HasKey(u => u.Id); // Primary key

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

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Task> Tasks { get; set; }
    }
}
