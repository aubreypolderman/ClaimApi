using Microsoft.EntityFrameworkCore;
using ClaimApi.Model;

namespace ClaimApi.Model
{
    public class ContractContext: DbContext
    {
        public ContractContext(DbContextOptions<ContractContext> options)
            : base(options)
        {
        }

        public DbSet<Contract> Contracts { get; set; } = null!;
        public DbSet<User> Users { get; set; }
        public DbSet<Claim> Claims { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
            modelBuilder.Entity<Contract>()
                .HasOne(e => e.User)
                .WithMany(e => e.Contracts)
                .HasForeignKey(e => e.UserId)
                .IsRequired();

            base.OnModelCreating(modelBuilder);
        }
    }
}
