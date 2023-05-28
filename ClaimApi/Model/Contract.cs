using System.Text.Json.Serialization;

namespace ClaimApi.Model
{
    public class Contract
    {
        public int Id { get; set; }
        public int UserId { get; set; } // Foreign key to User table
        public string Product { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public string LicensePlate { get; set; }
        public int DamageFreeYears { get; set; }
        public DateTime StartingDate { get; set; }
        public DateTime EndDate { get; set; }
        public double AnnualPolicyPremium { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        
        public User User { get; set; } = null!; // Navigation property
    }
}
