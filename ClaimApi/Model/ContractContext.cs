using Microsoft.EntityFrameworkCore;

namespace ClaimApi.Model
{
    public class ContractContext: DbContext
    {
        public ContractContext(DbContextOptions<ContractContext> options)
            : base(options)
        {
        }

        public DbSet<Contract> Contracts { get; set; } = null!;   
    }
}
