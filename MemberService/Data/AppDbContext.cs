using MemberService.Models;
using Microsoft.EntityFrameworkCore;

namespace MemberService.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            
        }


        // Members property is defined here which is a DbSet is a mediator between the Member model and the database table
        // DbSet represents the collection of all Members in the context, or that can be queried from the database
        public DbSet<Member> Members { get; set; }
    } 
}