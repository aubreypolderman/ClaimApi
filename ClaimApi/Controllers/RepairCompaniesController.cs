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

        // POST: api/RepairCompanies
        [HttpPost]
        public async Task<ActionResult<RepairCompany>> PostRepairCompany(RepairCompany repairCompany)
        {
            if (_context.RepairCompanies == null)
            {
                return Problem("Entity set 'RepairCompanyContext.RepairCompany' is null.");
            }
            _context.RepairCompanies.Add(repairCompany);
            await _context.SaveChangesAsync();

            // 13-03-2023 replace with nameof
            return CreatedAtAction(nameof(GetRepairCompany), new { id = repairCompany.Id }, repairCompany);
        }

        // GET: api/RepairCompanies
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RepairCompany>>> GetRepairCompanies()
        {
            if (_context.RepairCompanies == null)
            {
                return NotFound();
            }
            return await _context.RepairCompanies.ToListAsync();
        }

        // GET: api/RepairCompanies/1
        [HttpGet("{id}")]
        public async Task<ActionResult<RepairCompany>> GetRepairCompany(int id)
        {
            if (_context.RepairCompanies == null)
            {
                return NotFound();
            }
            var repairCompany = await _context.RepairCompanies.FindAsync(id);

            if (repairCompany == null)
            {
                return NotFound();
            }

            return repairCompany;
        }

        // DELETE: api/RepairCompanies/1
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

        // PUT: api/RepairCompanies/1
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

        private bool RepairCompanyExists(int id)
        {
            return (_context.RepairCompanies?.Any(e => e.Id == id)).GetValueOrDefault();
        }


    }
}
