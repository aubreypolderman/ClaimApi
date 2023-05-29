using ClaimApi.Model;

namespace ClaimApi.Repository;

public interface IRepairCompanyRepository
{
    Task<IEnumerable<RepairCompany>> GetAllRepairCompanies();
    Task<RepairCompany> GetRepairCompany(int id);
    Task<RepairCompany> CreateRepairCompany(RepairCompany repairCompany);
    Task<bool> UpdateRepairCompany(RepairCompany repairCompany);
    Task<bool> DeleteRepairCompany(int id);

}
