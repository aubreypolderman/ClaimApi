using ClaimApi.Model;
using ClaimApi.Repository;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

public class RepairCompanyRepository : IRepairCompanyRepository
{
    private readonly RepairCompanyContext _context;

    public RepairCompanyRepository(RepairCompanyContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<RepairCompany>> GetAllRepairCompanies()
    {
        Debug.WriteLine(DateTime.Now + "[--------] [RepairCompanyRepository] start ");
        return await _context.RepairCompanies.ToListAsync();
        
    }

    public async Task<RepairCompany> GetRepairCompany(int id)
    {
        return await _context.RepairCompanies.FindAsync(id);
    }

    public async Task<RepairCompany> CreateRepairCompany(RepairCompany repairCompany)
    {
        _context.RepairCompanies.Add(repairCompany);
        await _context.SaveChangesAsync();
        return repairCompany;
    }

    public async Task<bool> UpdateRepairCompany(RepairCompany repairCompany)
    {
        _context.Entry(repairCompany).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteRepairCompany(int id)
    {
        var repairCompany = await _context.RepairCompanies.FindAsync(id);
        if (repairCompany == null)
            return false;

        _context.RepairCompanies.Remove(repairCompany);
        await _context.SaveChangesAsync();
        return true;
    }
}
