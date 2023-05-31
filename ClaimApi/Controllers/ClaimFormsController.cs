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
using System.Diagnostics.Contracts;

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
            var claimForms = await _claimFormRepository.GetAllClaimForms();

            // Create a list to store ClaimFormDto objects
            var claimFormDtos = new List<ClaimFormDto>();

            foreach (var claimForm in claimForms)
            {
                // Get the Contract associated with the ClaimForm
                var contract = await _contractRepository.GetContract(claimForm.ContractId);
                if (contract == null)
                {
                    // Skip this ClaimForm if the associated Contract is not found
                    continue;
                }

                // Map the ClaimForm model to a ClaimFormDto
                var claimFormDto = new ClaimFormDto
                {
                    Id = claimForm.Id,
                    DateOfOccurence = claimForm.DateOfOccurence,
                    QCauseOfDamage = claimForm.QCauseOfDamage,
                    QWhatHappened = claimForm.QWhatHappened,
                    QWhereDamaged = claimForm.QWhereDamaged,
                    QWhatIsDamaged = claimForm.QWhatIsDamaged,
                    Image1 = claimForm.Image1,
                    Image2 = claimForm.Image2,
                    Street = claimForm.Street,
                    Suite = claimForm.Suite,
                    City = claimForm.City,
                    Zipcode = claimForm.Zipcode,
                    Latitude = claimForm.Latitude,
                    Longitude = claimForm.Longitude,
                    ContractId = claimForm.ContractId,
                    Contract = contract
                };

                // Add the ClaimFormDto to the list
                claimFormDtos.Add(claimFormDto);
            }

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

        [HttpGet("{id}")]
        public async Task<ActionResult<ClaimFormDto>> GetClaimForm(int id)
        {
            var claimForm = await _claimFormRepository.GetClaimForm(id);
            if (claimForm is null)
                return NotFound("ClaimForm not found.");

            // Get the Contract associated with the ClaimForm
            var contract = await _contractRepository.GetContract(claimForm.ContractId);
            if (contract == null)
            {
                // Skip this ClaimForm if the associated Contract is not found
                return NotFound("Contract not found.");
            }
            // Map the Contract model to a ContractDto
            var claimFormDto = new ClaimFormDto
            {
                Id = claimForm.Id,
                DateOfOccurence = claimForm.DateOfOccurence,
                QCauseOfDamage = claimForm.QCauseOfDamage,
                QWhatHappened = claimForm.QWhatHappened,
                QWhereDamaged = claimForm.QWhereDamaged,
                QWhatIsDamaged = claimForm.QWhatIsDamaged,
                Image1 = claimForm.Image1,
                Image2 = claimForm.Image2,
                Street = claimForm.Street,
                Suite = claimForm.Suite,
                City = claimForm.City,
                Zipcode = claimForm.Zipcode,
                Latitude = claimForm.Latitude,
                Longitude = claimForm.Longitude,
                ContractId = claimForm.ContractId,
                Contract = contract
            };

            return Ok(claimFormDto);
        }

    }
}