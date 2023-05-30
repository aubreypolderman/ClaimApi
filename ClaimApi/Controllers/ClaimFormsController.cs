using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ClaimApi.Model;
using ClaimApi.Repository;
using System.Text.Json.Serialization;
using System.Diagnostics;

namespace ClaimApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClaimFormsController : ControllerBase
    {
        private readonly IClaimFormRepository _claimFormRepository;
        private readonly IContractRepository _contractRepository;

        public ClaimFormsController(IClaimFormRepository claimFormRepository, IContractRepository contractRepository)
        {
            _claimFormRepository = claimFormRepository;
            _contractRepository = contractRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ClaimFormDto>>> GetClaims()
        {
            Debug.WriteLine("[..............] [ClaimFormController] [GetClaims] Before invoke of repository ");
            var claimForms = await _claimFormRepository.GetAllClaimForms();
            Debug.WriteLine("[..............] [ClaimFormController] [GetClaims] After invoke of repository " + claimForms);

            // Map the claimForm models to ClaimFormDtos
            var claimFormDtos = claimForms.Select(c => new ClaimFormDto
            {
                Id = c.Id,
                DateOfOccurence = c.DateOfOccurence,
                QCauseOfDamage = c.QCauseOfDamage,
                QWhatHappened = c.QWhatHappened,
                QWhereDamaged = c.QWhereDamaged,
                QWhatIsDamaged = c.QWhatIsDamaged,
                Image1 = c.Image1,
                Image2 = c.Image2,
                Street = c.Street,
                Suite = c.Suite,
                City = c.City,
                Zipcode = c.Zipcode,
                Latitude = c.Latitude,
                Longitude = c.Longitude,
                ContractId = c.ContractId
            });

            return Ok(claimFormDtos);
        }      

        [HttpGet("user/{userId}")]
        public async Task<ActionResult<IEnumerable<ClaimFormDto>>> GetClaimsByUserId(int userId)
        {
            var claims = await _claimFormRepository.GetClaimFormsByUserId(userId);
            return Ok(claims);
        }

        [HttpPost]
        public async Task<ActionResult<ClaimForm>> CreateClaimForm(ClaimForm claimForm)
        {
            Debug.WriteLine("[..............] [ClaimFormController] [CreateClaimForm] Make claim for contract with id " + claimForm.ContractId);
            Debug.WriteLine("[..............] [ClaimFormController] [CreateClaimForm] Make claim for contract with id " + claimForm.Contract.Id);
            claimForm.ContractId = claimForm.Contract.Id; // Set the ContractId based on the request's userId value

            // Get the Contract from the ContractRepository
            var contract = await _contractRepository.GetContract(claimForm.ContractId);
            if (contract == null)
            {
                return BadRequest("Contract does not exist");
            }

            claimForm.Contract = null; // Set the Contract property to null since it's not needed            

            var createdClaimForm = await _claimFormRepository.CreateClaimForm(claimForm);
            return CreatedAtAction(nameof(CreateClaimForm), new { id = createdClaimForm.Id }, createdClaimForm);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateClaimForm(int id, ClaimForm claimForm)
        {
            if (id != claimForm.Id)
                return BadRequest();

            var result = await _claimFormRepository.UpdateClaimForm(claimForm);

            if (result)
                return NoContent();
            else
                return NotFound();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteClaimForm(int id)
        {
            var result = await _claimFormRepository.DeleteClaimForm(id);
            if (!result)
                return NotFound();

            return NoContent();
        }

    }
}