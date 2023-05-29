namespace ClaimApi.Model
{
    public class ContractDto
    {
        public int Id { get; set; }
        public string Product { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public string LicensePlate { get; set; }
        public int DamageFreeYears { get; set; }
        public DateTime StartingDate { get; set; }
        public DateTime EndDate { get; set; }
        public double AnnualPolicyPremium { get; set; }


    }
}
