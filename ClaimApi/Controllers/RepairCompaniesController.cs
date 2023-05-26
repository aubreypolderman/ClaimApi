using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ClaimApi.Model;

namespace ClaimApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RepairCompaniesController : ControllerBase
    {
        private readonly RepairCompanyContext _context;

        public RepairCompaniesController(RepairCompanyContext context)
        {
            _context = context;
        }

        // GET: api/RepairCompanys
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RepairCompany>>> GetRepairCompany()
        {
          if (_context.RepairCompanies == null)
          {
              return NotFound();
          }
            return await _context.RepairCompanies.ToListAsync();
        }

        // GET: api/RepairCompanys/5
        [HttpGet("{id}")]
        public async Task<ActionResult<RepairCompany>> GetRepairCompany(int id)
        {
          if (_context.RepairCompanies == null)
          {
              return NotFound();
          }
            var repaircompany = await _context.RepairCompanies.FindAsync(id);

            if (repaircompany == null)
            {
                return NotFound();
            }

            return repaircompany;
        }

        // PUT: api/RepairCompanys/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRepairCompany(int id, RepairCompany repaircompany)
        {
            if (id != repaircompany.Id)
            {
                return BadRequest();
            }

            _context.Entry(repaircompany).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RepairCompanyExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/RepairCompanys
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<RepairCompany>> PostRepairCompany(RepairCompany repaircompany)
        {
          if (_context.RepairCompanies == null)
          {
              return Problem("Entity set 'RepairCompanyContext.RepairCompany'  is null.");
          }
            _context.RepairCompanies.Add(repaircompany);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetGarage", new { id = repaircompany.Id }, repaircompany);
        }

        // DELETE: api/Garages/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRepairCompany(int id)
        {
            if (_context.RepairCompanies == null)
            {
                return NotFound();
            }
            var repaircompany = await _context.RepairCompanies.FindAsync(id);
            if (repaircompany == null)
            {
                return NotFound();
            }

            _context.RepairCompanies.Remove(repaircompany);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool RepairCompanyExists(int id)
        {
            return (_context.RepairCompanies?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
