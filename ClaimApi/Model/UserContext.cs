using ClaimApi.Data;
using Microsoft.EntityFrameworkCore;

namespace ClaimApi.Model
{
    public class UserContext: DbContext
    {
        public UserContext(DbContextOptions<UserContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasData(
                new User
                {
                    Id= 1,
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
            //DataSeeder.SeedData(DbContext UserContext);


        }
    }
}
