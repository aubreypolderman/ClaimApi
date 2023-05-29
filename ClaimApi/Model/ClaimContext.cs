using Microsoft.EntityFrameworkCore;

namespace ClaimApi.Model;
public class ClaimContext: DbContext
{
    public ClaimContext(DbContextOptions<ClaimContext> options)
        : base(options)
    {
    }

    public DbSet<Claim> Claims { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Claim>()
            .HasOne(cl => cl.Contract)
            .WithMany(c => c.Claims)
            .HasForeignKey(cl => cl.ContractId)
            .IsRequired();

        base.OnModelCreating(modelBuilder);
    }
}


