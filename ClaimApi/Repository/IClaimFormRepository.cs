using ClaimApi.Model;

namespace ClaimApi.Repository;

public interface IClaimFormRepository
{
    Task<IEnumerable<ClaimForm>> GetAllClaimForms();    
    Task<IEnumerable<ClaimForm>> GetClaimFormsByUserId(int userId);
    Task<ClaimForm> CreateClaimForm(ClaimForm claimForm);
    Task<ClaimForm> GetClaimForm(int id);
    Task<bool> UpdateClaimForm(ClaimForm claimForm);
    Task<bool> DeleteClaimForm(int id);
    Task<IEnumerable<ClaimForm>> GetClaimFormsByContractIds(List<int> contractIds);

}
