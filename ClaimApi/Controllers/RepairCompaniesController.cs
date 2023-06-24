using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ClaimApi.Model;
using ClaimApi.Repository;
using Microsoft.AspNetCore.Authorization;
using Azure;
using System.Diagnostics;

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
        [Authorize]
        public async Task<ActionResult<IEnumerable<RepairCompany>>> GetRepairCompanies()
        {
            Debug.WriteLine(DateTime.Now + "[--------] [RepairCompanyController] repaircompanies ");
            Debug.WriteLine(DateTime.Now + "[.........][RepairCompanyController] [GetRepairCompanies] Start");
            var repaircompanies = await _repairCompanyRepository.GetAllRepairCompanies();
            Debug.WriteLine(DateTime.Now + "[.........][RepairCompanyController] [GetRepairCompanies] repaircompanies count = " + repaircompanies.Count());
            return Ok(repaircompanies);
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult<RepairCompany>> GetRepairCompany(int id)
        {
            var repairCompany = await _repairCompanyRepository.GetRepairCompany(id);
            if (repairCompany is null)
                return NotFound("RepairCompany not found.");

            return Ok(repairCompany);
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult<RepairCompany>> CreateRepairCompany(RepairCompany repairCompany)
        {
            var createdRepairCompany = await _repairCompanyRepository.CreateRepairCompany(repairCompany);
            return CreatedAtAction(nameof(CreateRepairCompany), new { id = createdRepairCompany.Id }, createdRepairCompany);
        }

        [HttpPut("{id}")]
        [Authorize]
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
        [Authorize]
        public async Task<IActionResult> DeleteRepairCompanyt(int id)
        {
            var result = await _repairCompanyRepository.DeleteRepairCompany(id);
            if (!result)
                return NotFound();

            return NoContent();
        }
    }
}