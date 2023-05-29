using ClaimApi.Data;
using ClaimApi.Model;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;

namespace ClaimApi.Model
{
    public class UserContext : DbContext
    {
        public UserContext(DbContextOptions<UserContext> options)
            : base(options)
        {
            // Empty constructor
        }
      
        public DbSet<User> Users { get; set; }
        public DbSet<Contract> Contracts { get; set; }
        public DbSet<Claim> Claims { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                        .HasMany(e => e.Contracts)
                        .WithOne(e => e.User)
                        .HasForeignKey(e => e.UserId)
                        .IsRequired();

            modelBuilder.Entity<Contract>()
                        .HasMany(e => e.Claims)
                        .WithOne(e => e.Contract)
                        .HasForeignKey(e => e.ContractId)
                        .IsRequired();
        }       
    }
}
