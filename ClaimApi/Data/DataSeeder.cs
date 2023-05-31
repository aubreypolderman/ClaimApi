using ClaimApi.Data;
using ClaimApi.Model;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Diagnostics;

namespace ClaimApi.Data;

public class DataSeeder
{

    public static void SeedData(UserContext userContext, RepairCompanyContext repairCompanyContext)
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
                    Username = "aubreypolderman@gmail.com",
                    Email = "aubreypolderman@gmail.com",
                    Phone = "06-12345678",
                    Street = "Cirkel",
                    HouseNumber = "63",
                    City = "Vlissingen",
                    Zipcode = "4384DS",
                    Latitude = 51.461684899386995,
                    Longitude = 3.5559567820729203
                },
                new User
                {
                    Name = "Captain America",
                    Username = "captainamerica@outlook.com",
                    Email = "captainamerica@outlook.com",
                    Phone = "06-11223344",
                    Street = "Waalstraat",
                    HouseNumber = "70",
                    City = "Middelburg",
                    Zipcode = "4335KN",
                    Latitude = 51.461684899386995,
                    Longitude = 3.5559567820729203
                },
                new User
                {
                    Name = "Pete Mitchell",
                    Username = "petemitchell@outlook.com",
                    Email = "petemitchell@outlook.com",
                    Phone = "06-55667788",
                    Street = "Beverstraat",
                    HouseNumber = "9C",
                    City = "Rotterdam",
                    Zipcode = "3074SC",
                    Latitude = 51.461684899386995,
                    Longitude = 3.5559567820729203
                },
                                 
        };
        Debug.WriteLine(DateTime.Now + "[--------] [DataSeeder] user aangemaakt");
        userContext.Users.AddRange(users);
        userContext.SaveChanges();
        }

        // Check if data already exists
        if (!repairCompanyContext.RepairCompanies.Any())
        {

            // Seed the data
            var repairCompanies = new List<RepairCompany>
            {
                new RepairCompany
                {
                    Name = "Van den Berg autoschade",
                    Email = "info@vdberg.nl",
                    Street = "Beijerselaan",
                    Housenumber = "80",
                    City = "Rotterdam",
                    Zipcode = "3074SN",
                    Latitude = 51.8974572425449,
                    Longitude = 4.511208184602901,
                    Phone = "0113-238000",
                    Website = "www.vdberg.nl"
                },
                new RepairCompany
                {
                    Name = "VanMossel autoschade",
                    Email = "info@mossel.nl",
                    Street = "Slaghekstraat",
                    Housenumber = "54A",
                    City = "Rotterdam",
                    Zipcode = "3074LN",
                    Latitude = 51.89696070595346,
                    Longitude = 4.514330275773976,
                    Phone = "0113-238111",
                    Website = "www.mossel.nl"
                },
                new RepairCompany
                {
                    Name = "Borg Schadenet",
                    Email = "info@borg.nl",
                    Street = "Piersonstraat",
                    Housenumber = "20",
                    City = "Schiedam",
                    Zipcode = "3119GR",
                    Latitude = 51.92275242481925, 
                    Longitude = 4.387309843433312,
                    Phone = "0113-238222",
                    Website = "www.borg.nl"
                },
                new RepairCompany
                {
                    Name = "Duinkerk Schadenet",
                    Email = "info@duinkerk.nl",
                    Street = "Ambachtsweg",
                    Housenumber = "1",
                    City = "Rhoon",
                    Zipcode = "3161GL",
                    Latitude = 51.86404076058215,
                    Longitude = 4.434807604412547,
                    Phone = "0113-238333",
                    Website = "www.duinkerk.nl"
                },
                new RepairCompany
                {
                    Name = "Verhoeven autoschade",
                    Email = "info@verhoeven.nl",
                    Street = "Timorstraat",
                    Housenumber = "5",
                    City = "Dordrecht",
                    Zipcode = "3312CW",
                    Latitude = 51.8167363800586, 
                    Longitude = 4.683671705969263,
                    Phone = "0113-238444",
                    Website = "www.duinkerk.nl"
                },
                new RepairCompany
                {
                    Name = "Tiggelen autoschade",
                    Email = "info@tiggelen.nl",
                    Street = "Wisselaar",
                    Housenumber = "2",
                    City = "Breda",
                    Zipcode = "4826AG",
                    Latitude = 51.62057483916424, 
                    Longitude = 4.77710457732913,
                    Phone = "0113-238555",
                    Website = "www.tiggelen.nl"
                },
        };
            Debug.WriteLine(DateTime.Now + "[--------] [DataSeeder] repaircompanies aangemaakt");
            repairCompanyContext.RepairCompanies.AddRange(repairCompanies);
            repairCompanyContext.SaveChanges();
        }
    }
}
