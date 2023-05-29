using ClaimApi.Model;
using ClaimApi.Repository;
using Microsoft.EntityFrameworkCore;

public class ClaimFormRepository : IClaimFormRepository
{
    private readonly ClaimFormContext _context;

    public ClaimFormRepository(ClaimFormContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<ClaimForm>> GetAllClaimForms()
    {
        return await _context.ClaimForms.ToListAsync();
    }
   
    public async Task<IEnumerable<ClaimForm>> GetClaimFormsByUserId(int userId)
    {
        return await _context.ClaimForms
            .Where(c => c.ContractId == userId)
            .ToListAsync();
    }

    public async Task<ClaimForm> CreateClaimForm(ClaimForm claimForm)
    {
        _context.ClaimForms.Add(claimForm);
        await _context.SaveChangesAsync();
        return claimForm;
    }

    public async Task<bool> UpdateClaimForm(ClaimForm claimForm)
    {
        _context.Entry(claimForm).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteClaimForm(int id)
    {
        var claimForm = await _context.ClaimForms.FindAsync(id);
        if (claimForm == null)
            return false;

        _context.ClaimForms.Remove(claimForm);
        await _context.SaveChangesAsync();
        return true;
    }
}
