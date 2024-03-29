﻿using System.Text.Json.Serialization;

namespace ClaimApi.Model;
public class RepairCompany
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; }

    [JsonPropertyName("email")]
    public string Email { get; set; }

    [JsonPropertyName("street")]
    public string Street { get; set; }

    [JsonPropertyName("housenumber")]
    public string Housenumber { get; set; }

    [JsonPropertyName("city")]
    public string City { get; set; }

    [JsonPropertyName("zipcode")]
    public string Zipcode { get; set; }

    [JsonPropertyName("latitude")]
    public double Latitude { get; set; }

    [JsonPropertyName("longitude")]
    public double Longitude { get; set; }

    [JsonPropertyName("phone")]
    public string Phone { get; set; }

    [JsonPropertyName("website")]
    public string Website { get; set; }
}

