using Microsoft.EntityFrameworkCore;
using TripService.Models;

namespace TripService.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options): base(options)
        {
            
        }

        public DbSet<Member> Members {get; set;} = null!;   
        public DbSet<Trip> Trips { get; set; } = null!;

        // Configure the model relationships here if needed
        // For example, setting up one-to-many relationship between Member and Trip
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Tell EF Core that Member.MemberId is a valid key for relationships
            modelBuilder
                .Entity<Member>()
                .HasMany(m => m.Trips)
                .WithOne(t => t.Member)
                .HasPrincipalKey(t => t.MemberId); // Alternate Key on the Member entity

            modelBuilder
                .Entity<Trip>()
                .HasOne(t => t.Member)
                .WithMany(t => t.Trips)
                .HasForeignKey(t => t.MemberId)  // FK on the Trip entity
                .HasPrincipalKey(t => t.MemberId); // Key on the Member entity
        }
    }
}