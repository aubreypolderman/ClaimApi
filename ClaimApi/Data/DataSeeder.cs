using ClaimApi.Data;
using ClaimApi.Model;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ClaimApi.Data
{
    public class DataSeeder
    {

        public static void SeedData(UserContext userContext)
        {
            // Check if data already exists
            if (!userContext.Users.Any())
            {

                // Seed the data
                var users = new List<User>
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
                        Latitude = 51.461684899386995,
                        Longitude = 3.5559567820729203
                    },
                    // Add more users if needed
                };

                userContext.Users.AddRange(users);
                userContext.SaveChanges();
                // await _context.SaveChangesAsync();
            }
        }
    }
}
