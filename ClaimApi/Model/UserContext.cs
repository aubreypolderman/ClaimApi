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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder);
        }

        /*
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            if (!Users.Any())
            {
                modelBuilder.Entity<User>().HasData(
                new User
                {
                    Id = 1,
                    Name = "Aubrey Polderman",
                    Username = "aubreypolderman",
                    Email = "aubreypolderman@zlm.nl",
                    Phone = "123456789",
                    Street = "Cirkel",
                    HouseNumber = "63",
                    City = "Vlissingen",
                    Zipcode = "4384DS",
                    Latitude = "12544.025",
                    Longitude = "54880.450"
                }
                );
            }
        }
        */
    }
}
