﻿using ClaimApi.Data;
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

    public static void SeedData(UserContext userContext, RepairCompanyContext repairCompanyContext, ContractContext contractContext, ClaimFormContext claimFormContext)
    {
        // Check if data already exists
        if (!userContext.Users.Any())
        {

            // Seed the data
            var users = new List<User>
            {
                new User
                {
                    Id = 1,
                    Name = "Aubrey Polderman",
                    Username = "Aubrey Polderman",
                    Email = "Aubrey Polderman",
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
                    Id = 2,
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
                    Id = 3,
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
            Debug.WriteLine(DateTime.Now + "[--------] [DataSeeder] user added");
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
                    Id = 1,
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
                    Id = 2,
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
                    Id = 3,
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
                    Id = 4,
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
                    Id = 5,
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
                    Id = 6,
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
            Debug.WriteLine(DateTime.Now + "[--------] [DataSeeder] repaircompanies added");
            repairCompanyContext.RepairCompanies.AddRange(repairCompanies);
            repairCompanyContext.SaveChanges();
        }
        
        // Check if data already exists
        if (!contractContext.Contracts.Any())
        {

            // Seed the data
            var contracts = new List<Contract>
            {
                new Contract
                {
                    Id = 1,
                    Product = "Personenauto",
                    Make = "KIA",
                    Model = "Ceed",
                    LicensePlate = "HF067X",
                    DamageFreeYears = 15,
                    StartingDate = new DateTime(2023, 6, 1, 00, 00, 00),
                    EndDate = new DateTime(2023, 12, 31, 00, 00, 00),
                    AnnualPolicyPremium = 150.99,
                    UserId = 1
                },
                new Contract
                {
                    Id = 2,
                    Product = "Personenauto",
                    Make = "Lamborghini",
                    Model = "Diablo",
                    LicensePlate = "R789RF",
                    DamageFreeYears = 15,
                    StartingDate = new DateTime(2023, 6, 1, 00, 00, 00),
                    EndDate = new DateTime(2023, 12, 31, 00, 00, 00),
                    AnnualPolicyPremium = 750.49,
                    UserId = 1
                },
                new Contract
                {
                    Id = 3,
                    Product = "Personenauto",
                    Make = "Audi",
                    Model = "Q6",
                    LicensePlate = "T988RX",
                    DamageFreeYears = 11,
                    StartingDate = new DateTime(2023, 1, 1, 00, 00, 00),
                    EndDate = new DateTime(2023, 12, 31, 00, 00, 00),
                    AnnualPolicyPremium = 599.49,
                    UserId = 2
                },
                new Contract
                {
                    Id = 4,
                    Product = "Personenauto",
                    Make = "Porsche",
                    Model = "Cayenne",
                    LicensePlate = "X988RR",
                    DamageFreeYears = 3,
                    StartingDate = new DateTime(2022, 8, 12, 00, 00, 00),
                    EndDate = new DateTime(2023, 12, 31, 00, 00, 00),
                    AnnualPolicyPremium = 1599.49,
                    UserId = 3
                }
            };
            Debug.WriteLine(DateTime.Now + "[--------] [DataSeeder] contracts added");
            contractContext.Contracts.AddRange(contracts);
            contractContext.SaveChanges();
        }

        // Check if data already exists
        if (!claimFormContext.ClaimForms.Any())
        {

            DateTime fixedDateTime1 = new DateTime(2023, 6, 1, 15, 26, 35);
            DateTime fixedDateTime2 = new DateTime(2023, 5, 30, 23, 58, 11);
            DateTime fixedDateTime3 = new DateTime(2023, 3, 25, 14, 13, 11);
            // Seed the data
            var claimForms = new List<ClaimForm>
            {
                new ClaimForm
                {
                    Id = 1,                    
                    DateOfOccurence = fixedDateTime1,
                    QCauseOfDamage = "Aanrijding met een vast object",
                    QWhereDamaged = "Somewhere at night",
                    QWhatIsDamaged = "The doors en trunk of my lamborghini are damaged. And the windshields are broken",
                    Image1 = null,
                    Image2 = null,
                    Street = "Hoofdweg",
                    Suite = "2",
                    City = "Rotterdam",
                    Zipcode = "3067GK",
                    Latitude = 51.95059777234066, 
                    Longitude = 4.562103388325112,
                    ContractId = 1
                },
                new ClaimForm
                {
                    Id = 2,
                    DateOfOccurence = fixedDateTime2,
                    QCauseOfDamage = "Aanrijding met een vast object",
                    QWhereDamaged = "Where is the damage",
                    QWhatIsDamaged = "Everything is broken!",
                    Image1 = null,
                    Image2 = null,
                    Street = "Pleinweg",
                    Suite = "12",
                    City = "Rotterdam",
                    Zipcode = "3067GK",
                    Latitude = 51.95059777234066,
                    Longitude = 4.562103388325112,
                    ContractId = 1
                },
                new ClaimForm
                {
                    Id = 3,
                    DateOfOccurence = fixedDateTime3,
                    QCauseOfDamage = "Ruitschade",
                    QWhereDamaged = "Where is the damage",
                    QWhatIsDamaged = "Everything is broken!",
                    Image1 = null,
                    Image2 = null,
                    Street = "Coolsingel",
                    Suite = "12",
                    City = "Rotterdam",
                    Zipcode = "3068GK",
                    Latitude = 51.95059777234066,
                    Longitude = 4.562103388325112,
                    ContractId = 2
                },
                new ClaimForm
                {
                    Id = 4,
                    DateOfOccurence = fixedDateTime3,
                    QCauseOfDamage = "Ruitschade",
                    QWhereDamaged = "Where is the damage",
                    QWhatIsDamaged = "Everything is broken!",
                    Image1 = null,
                    Image2 = null,
                    Street = "Waterlelie",
                    Suite = "88",
                    City = "Rotterdam",
                    Zipcode = "3022GK",
                    Latitude = 51.95059777234066,
                    Longitude = 4.562103388325112,
                    ContractId = 4
                },
            };
            Debug.WriteLine(DateTime.Now + "[--------] [DataSeeder] claimforms added");
            claimFormContext.ClaimForms.AddRange(claimForms);
            claimFormContext.SaveChanges();
        }
    }
}
