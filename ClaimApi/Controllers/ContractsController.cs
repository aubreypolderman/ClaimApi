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
    public class ContractsController : ControllerBase
    {
        private readonly IContractRepository _contractRepository;

        public ContractsController(IContractRepository contractRepository)
        {
            _contractRepository = contractRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Contract>>> GetContracts()
        {
            var contracts = await _contractRepository.GetAllContracts();
            return Ok(contracts);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Contract>> GetContract(int id)
        {
            var contract = await _contractRepository.GetContract(id);
            if (contract is null)
                return NotFound("Contract not found.");

            return Ok(contract);
        }

        [HttpPost]
        public async Task<ActionResult<Contract>> CreateContract(Contract contract)
        {
            contract.UserId = contract.User.Id; // Set the UserId based on the request's userId value
            contract.User = null; // Set the User property to null since it's not needed

            var createdContract = await _contractRepository.CreateContract(contract);
            return CreatedAtAction(nameof(CreateContract), new { id = createdContract.Id }, createdContract);            
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateContract(int id, Contract contract)
        {
            if (id != contract.Id)
                return BadRequest();

            var result = await _contractRepository.UpdateContract(contract);

            if (result)
                return NoContent();
            else
                return NotFound();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteContract(int id)
        {
            var result = await _contractRepository.DeleteContract(id);
            if (!result)
                return NotFound();

            return NoContent();
        }

        [HttpGet("user/{userId}")]
        public async Task<ActionResult<IEnumerable<Contract>>> GetContractsByUserId(int userId)
        {
            var contracts = await _contractRepository.GetContractsByUserId(userId);
            return Ok(contracts);
        }

    }
}