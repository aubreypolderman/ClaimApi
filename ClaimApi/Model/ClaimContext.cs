using Microsoft.EntityFrameworkCore;

namespace ClaimApi.Model
{
    public class ClaimContext: DbContext
    {
        public ClaimContext(DbContextOptions<ClaimContext> options)
            : base(options)
        {
        }

        public DbSet<Claim> Claims { get; set; } = null!;   
    }
}
