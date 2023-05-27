using System.IO;
using System.Reflection.Emit;
using System.Text.Json.Serialization;

namespace ClaimApi.Model;

public class User
{
    public int Id { get; set; }

    public string Name { get; set; }

    public string Username { get; set; }
   
    public string Email { get; set; }
    public string Phone { get; set; }

    public string Street { get; set; }

    public string HouseNumber { get; set; }

    public string City { get; set; }

    public string Zipcode { get; set; }

    public double Latitude { get; set; }

    [JsonPropertyName("longitude")]
    public double Longitude { get; set; }

    [JsonIgnore]
    public ICollection<Contract> Contracts { get; set; } = new List<Contract>();

}
