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
    public class ContractsController : ControllerBase
    {
        private readonly ContractContext _context;

        public ContractsController(ContractContext context)
        {
            _context = context;
        }

        // GET: api/Contracts
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Contract>>> GetContracts()
        {
          if (_context.Contracts == null)
          {
              return NotFound();
          }
            return await _context.Contracts.ToListAsync();
        }

        // GET: api/Contracts/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Contract>> GetContract(int id)
        {
          if (_context.Contracts == null)
          {
              return NotFound();
          }
            var contract = await _context.Contracts.FindAsync(id);

            if (contract == null)
            {
                return NotFound();
            }

            return contract;
        }

        // PUT: api/Contracts/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutContract(int id, Contract contract)
        {
            if (id != contract.Id)
            {
                return BadRequest();
            }

            _context.Entry(contract).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ContractExists(id))
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

        // POST: api/Contracts
        [HttpPost]
        public async Task<ActionResult<Contract>> PostContract(Contract contract)
        {
            try
            {
                // Verify that the UserId exists in the User model
                /*
                var user = await _context.Users.FindAsync(contract.UserId);
                if (user == null)
                {
                    return BadRequest("Invalid UserId");
                }*/
                // Validate the user object, excluding the 'Contracts' field
                ModelState.Remove("Contracts");
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                _context.Contracts.Add(contract);
                await _context.SaveChangesAsync();
                return CreatedAtAction(nameof(GetContract), new { id = contract.Id }, contract);
            }
            catch (Exception ex)
            {
                // Log the exception for troubleshooting
                Console.WriteLine("Error occurred while creating contract:=> " + ex);
                return StatusCode(500, "An error occurred while creating the contract");
            }
        }

        // DELETE: api/Contracts/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteContract(int id)
        {
            if (_context.Contracts == null)
            {
                return NotFound();
            }
            var contract = await _context.Contracts.FindAsync(id);
            if (contract == null)
            {
                return NotFound();
            }

            _context.Contracts.Remove(contract);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpGet("user/{userId}/contracts")]
        public async Task<ActionResult<IEnumerable<Contract>>> GetUserContracts(int userId)
        {
            try
            {
                var user = await _context.Users.Include(u => u.Contracts).FirstOrDefaultAsync(u => u.Id == userId);

                if (user == null)
                {
                    return NotFound($"User with ID {userId} not found");
                }

                var contracts = user.Contracts;

                return Ok(contracts);
            }
            catch (Exception ex)
            {
                // Log the exception for troubleshooting
                Console.WriteLine("Error occurred while retrieving contracts: " + ex);
                return StatusCode(500, "An error occurred while retrieving the contracts");
            }
        }

        private bool ContractExists(int id)
        {
            return (_context.Contracts?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
