using Microsoft.EntityFrameworkCore;

namespace ClaimApi.Model;
public class ClaimFormContext: DbContext
{
    public ClaimFormContext(DbContextOptions<ClaimFormContext> options)
        : base(options)
    {
    }

    public DbSet<ClaimForm> ClaimForms { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ClaimForm>()
            .HasOne(cl => cl.Contract)
            .WithMany(c => c.ClaimForms)
            .HasForeignKey(cl => cl.ContractId)
            .IsRequired();

        base.OnModelCreating(modelBuilder);
    }
}


