using System.IO;
using System.Reflection.Emit;

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

    public string Latitude { get; set; }

    public string Longitude { get; set; }

}
