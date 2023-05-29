using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ClaimApi.Model;
using ClaimApi.Repository;

namespace ClaimApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RepairCompaniesController : ControllerBase
    {
        private readonly IRepairCompanyRepository _repairCompanyRepository;

        public RepairCompaniesController(IRepairCompanyRepository repairCompanyRepository)
        {
            _repairCompanyRepository = repairCompanyRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<RepairCompany>>> GetRepairCompanies()
        {
            var repaircompanies = await _repairCompanyRepository.GetAllRepairCompanies();
            return Ok(repaircompanies);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<RepairCompany>> GetRepairCompany(int id)
        {
            var repairCompany = await _repairCompanyRepository.GetRepairCompany(id);
            if (repairCompany is null)
                return NotFound("RepairCompany not found.");

            return Ok(repairCompany);
        }

        [HttpPost]
        public async Task<ActionResult<RepairCompany>> CreateRepairCompany(RepairCompany repairCompany)
        {
            var createdRepairCompany = await _repairCompanyRepository.CreateRepairCompany(repairCompany);
            return CreatedAtAction(nameof(CreateRepairCompany), new { id = createdRepairCompany.Id }, createdRepairCompany);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateRepairCompany(int id, RepairCompany repairCompany)
        {
            if (id != repairCompany.Id)
                return BadRequest();

            var result = await _repairCompanyRepository.UpdateRepairCompany(repairCompany);
            if (!result)
                return NotFound();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRepairCompanyt(int id)
        {
            var result = await _repairCompanyRepository.DeleteRepairCompany(id);
            if (!result)
                return NotFound();

            return NoContent();
        }
    }
}