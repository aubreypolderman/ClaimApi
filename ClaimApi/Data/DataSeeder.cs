using ClaimApi.Model;
using Microsoft.EntityFrameworkCore;

namespace ClaimApi.Data;
    public class DataSeeder
    {
    public static void SeedData(DbContext context)
    {
        // Check if data already exists
        if (context.Set<User>().Any())
        {
            return; // Data has already been seeded
        }

        // Seed the data
        var users = new[]
        {
                new User
                {
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
                },
                // Add more users if needed
            };

        context.Set<User>().AddRange(users);
        context.SaveChanges();
    }
}
