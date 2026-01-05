using Microsoft.EntityFrameworkCore;
using TripService.Models;

namespace TripService.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options): base(options)
        {
            
        }

        public DbSet<Member> members {get; set;} = null!;   
        public DbSet<Trip> trips { get; set; } = null!;

        // Configure the model relationships here if needed
        // For example, setting up one-to-many relationship between Member and Trip
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<Member>()
                .HasMany(m => m.Trips)
                .WithOne(t => t.Member)
                .HasForeignKey(t => t.MemberId);

            modelBuilder
                .Entity<Trip>()
                .HasOne(t => t.Member)
                .WithMany(t => t.Trips)
                .HasForeignKey(t => t.MemberId);
        }
    }
}