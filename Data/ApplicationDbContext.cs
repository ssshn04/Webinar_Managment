using Microsoft.EntityFrameworkCore;
using WebinarManagement.Models;

namespace WebinarManagement.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Webinar> Webinars { get; set; }
        public DbSet<Participant> Participants { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Participant>()
                .HasOne(p => p.User)
                .WithMany(u => u.Participants)
                .HasForeignKey(p => p.UserID);

            modelBuilder.Entity<Participant>()
                .HasOne(p => p.Webinar)
                .WithMany(w => w.Participants)
                .HasForeignKey(p => p.WebinarID);
        }
    }
}
