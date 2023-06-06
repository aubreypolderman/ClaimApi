using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ClaimApi.Model;
using ClaimApi.Repository;
using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;

namespace ClaimApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContractsController : ControllerBase
    {
        private readonly IContractRepository _contractRepository;
        private readonly IUserRepository _userRepository;

        public ContractsController(IContractRepository contractRepository, IUserRepository userRepository)
        {
            _contractRepository = contractRepository;
            _userRepository = userRepository;
        }

        [HttpGet("private")]
        public async Task<ActionResult<IEnumerable<ContractDto>>> GetContracts()
        {
            var contracts = await _contractRepository.GetAllContracts();

            // Create a list to store ContractDto objects
            var contractDtos = new List<ContractDto>();

            foreach (var contract in contracts)
            {
                // Get the User entity associated with the contract
                var user = await _userRepository.GetUser(contract.UserId);
                if (user is null)
                {
                    // Skip this contract if the associated user is not found
                    continue;
                }

                // Map the Contract model to a ContractDto
                var contractDto = new ContractDto
                {
                    Id = contract.Id,
                    Product = contract.Product,
                    Make = contract.Make,
                    Model = contract.Model,
                    LicensePlate = contract.LicensePlate,
                    DamageFreeYears = contract.DamageFreeYears,
                    StartingDate = contract.StartingDate,
                    EndDate = contract.EndDate,
                    AnnualPolicyPremium = contract.AnnualPolicyPremium,
                    UserId = contract.UserId,
                    User = user
                };

                // Add the ContractDto to the list
                contractDtos.Add(contractDto);
            };

            return Ok(contractDtos);
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult<ContractDto>> GetContract(int id)
        {
            var contract = await _contractRepository.GetContract(id);
            if (contract is null)
                return NotFound("Contract not found.");

            // Get the User entity associated with the contract
            var user = await _userRepository.GetUser(contract.UserId);
            if (user is null)
                return NotFound("User not found.");

            // Map the Contract model to a ContractDto
            var contractDto = new ContractDto
            {
                Id = contract.Id,
                Product = contract.Product,
                Make = contract.Make,
                Model = contract.Model,
                LicensePlate = contract.LicensePlate,
                DamageFreeYears = contract.DamageFreeYears,
                StartingDate = contract.StartingDate,
                EndDate = contract.EndDate,
                AnnualPolicyPremium = contract.AnnualPolicyPremium,
                UserId = contract.UserId,
                User = user
            };

            return Ok(contractDto);
        }

        [HttpPost]
        public async Task<ActionResult<Contract>> CreateContract(Contract contract)
        {
            Debug.WriteLine("[..............] [ContractsController] [CreateContract] Make contract for user with id " + contract.UserId);
            contract.UserId = contract.User.Id; // Set the UserId based on the request's userId value
            contract.User = null; // Set the User property to null since it's not needed
            
            // Get the User from the UserRepository
            var user = await _userRepository.GetUser(contract.UserId);
            if (user == null)
            {
                return BadRequest("User does not exist");
            }

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
        [Authorize]
        public async Task<ActionResult<IEnumerable<ContractDto>>> GetContractsByUserId(int userId)
        {
            var contracts = await _contractRepository.GetContractsByUserId(userId);
            // Create a list to store ContractDto objects
            var contractDtos = new List<ContractDto>();

            foreach (var contract in contracts)
            {
                // Get the User entity associated with the contract
                var user = await _userRepository.GetUser(contract.UserId);
                if (user is null)
                {
                    // Skip this contract if the associated user is not found
                    continue;
                }

                // Map the Contract model to a ContractDto
                var contractDto = new ContractDto
                {
                    Id = contract.Id,
                    Product = contract.Product,
                    Make = contract.Make,
                    Model = contract.Model,
                    LicensePlate = contract.LicensePlate,
                    DamageFreeYears = contract.DamageFreeYears,
                    StartingDate = contract.StartingDate,
                    EndDate = contract.EndDate,
                    AnnualPolicyPremium = contract.AnnualPolicyPremium,
                    UserId = contract.UserId,
                    User = user
                };

                // Add the ContractDto to the list
                contractDtos.Add(contractDto);
            };

            return Ok(contractDtos);
            //return Ok(contracts);
        }

    }
}