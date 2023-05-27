using Microsoft.EntityFrameworkCore;

namespace ClaimApi.Model;

public class RepairCompanyContext: DbContext
{
    public RepairCompanyContext(DbContextOptions<RepairCompanyContext> options)
        : base(options)
    {
    }

    public DbSet<RepairCompany> RepairCompanies { get; set; } = null!;   
    
}

