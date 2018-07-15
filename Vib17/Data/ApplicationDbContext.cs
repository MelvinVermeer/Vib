using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Vib17.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // EF Core 2.0 ondersteunt nog geen many-to-many daarom een koppel class
            builder.Entity<UserSession>()
              .HasKey(t => new { t.SessionId, t.UserId });
            builder.Entity<UserSession>()
              .HasOne(pt => pt.Session)
              .WithMany(p => p.Attendees)
              .HasForeignKey(pt => pt.SessionId);
            builder.Entity<UserSession>()
              .HasOne(pt => pt.User)
              .WithMany(t => t.Sessions)
              .HasForeignKey(pt => pt.UserId);
        }

        public DbSet<Session> Sessions { get; set; }

        public DbSet<TimeSlot> Timeslots { get; set; }

        public DbSet<UserSession> UserSessions { get; set; }
    }
}
