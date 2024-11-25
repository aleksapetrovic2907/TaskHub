using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TaskHub.Models;

namespace TaskHub.Data
{
    public class AppDbContext : IdentityDbContext<ApplicationUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<UserTask> UserTasks { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<UserTask>()
                .HasOne(x => x.AssignedTo)
                .WithMany(x => x.UserTasks)
                .HasForeignKey(x => x.AssignedToId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
