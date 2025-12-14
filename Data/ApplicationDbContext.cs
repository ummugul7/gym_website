using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using proje.Models;

namespace proje.Data
{
    public class ApplicationDbContext : IdentityDbContext<Member>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Coach> Coach { get; set; }
        public DbSet<Appointment> Appointment { get; set; }

        // Data/ApplicationDbContext.cs

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Coach>()
                .HasOne(c => c.member)
                .WithMany() 
                .HasForeignKey(c => c.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            
            builder.Entity<Appointment>()
                .HasOne(a => a.Coach)
                .WithMany(c => c.Appointments)
                .HasForeignKey(a => a.CoachId)
                .OnDelete(DeleteBehavior.Restrict); // Koçu silerken randevuları silmeyi kısıtlar

           
            builder.Entity<Appointment>()
                .HasOne(a => a.Member)
                .WithMany(m => m.Appointments)
                .HasForeignKey(a => a.MemberId)
                .OnDelete(DeleteBehavior.Restrict); // Member'ı silerken randevuları silmeyi kısıtlar

        }
        public DbSet<proje.Models.CoachViewModel> CoachViewModel { get; set; } = default!;

    }
}
