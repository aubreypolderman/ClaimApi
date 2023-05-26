using Microsoft.EntityFrameworkCore;

namespace ClaimApi.Model;

public class RepairCompanyContext: DbContext
{
    public RepairCompanyContext(DbContextOptions<RepairCompanyContext> options)
        : base(options)
    {
    }

    public DbSet<RepairCompany> RepairCompanies { get; set; } = null!;   

    // public DbSet<ClaimApi.Model.RepairCompany> RepairCompany { get; set; } = default!;
}

