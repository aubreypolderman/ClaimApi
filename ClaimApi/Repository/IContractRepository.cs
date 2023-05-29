using ClaimApi.Model;

namespace ClaimApi.Repository;

public interface IContractRepository
{
    Task<IEnumerable<Contract>> GetAllContracts();
    Task<Contract> GetContract(int id);
    Task<Contract> CreateContract(Contract contract);
    Task<bool> UpdateContract(Contract contract);
    Task<bool> DeleteContract(int id);    
    Task<IEnumerable<Contract>> GetContractsByUserId(int userId);

}
