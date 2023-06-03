using ClaimApi.Model;
using ClaimApi.Repository;
using Microsoft.EntityFrameworkCore;

public class ContractRepository : IContractRepository
{
    private readonly ContractContext _context;

    public ContractRepository(ContractContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Contract>> GetAllContracts()
    {
        return await _context.Contracts.ToListAsync();
    }

    public async Task<Contract> GetContract(int id)
    {
        //var contract = await _context.Contracts.FindAsync(id);
        //return contract;
        return await _context.Contracts.FindAsync(id);
//        var contract = await _context.Contracts
    //.Include(c => c.User) // Join met het User-object
//    .FirstOrDefaultAsync(c => c.Id == id);

        //return contract;
    }

    public async Task<Contract> CreateContract(Contract contract)
    {
        _context.Contracts.Add(contract);
        await _context.SaveChangesAsync();
        return contract;
    }

    public async Task<bool> UpdateContract(Contract contract)
    {
        _context.Entry(contract).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteContract(int id)
    {
        var contract = await _context.Contracts.FindAsync(id);
        if (contract == null)
            return false;

        _context.Contracts.Remove(contract);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<IEnumerable<Contract>> GetContractsByUserId(int userId)
    {
        return await _context.Contracts
            .Where(c => c.UserId == userId)
            .ToListAsync();
    }

}
