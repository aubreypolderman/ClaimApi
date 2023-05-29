using ClaimApi.Model;
using ClaimApi.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ClaimApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]    
    public class UsersController : ControllerBase
    {
        private readonly IUserRepository _userRepository;

        public UsersController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }      

        [HttpGet]
        //[Authorize]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            var users = await _userRepository.GetAllUsers();
            return Ok(users);
        }

        [HttpGet("{id}")]
        //[Authorize]
        public async Task<ActionResult<User>> GetUser(int id)
        {
            var result = await _userRepository.GetUser(id);
            if (result is null)
                return NotFound("User not found.");

            return Ok(result);
        }

        [HttpPost]
        //[HttpPost("private-scoped")]
        //[Authorize("read:messages")]
        public async Task<ActionResult<User>> CreateUser(User user)
        {
            var createdUser = await _userRepository.CreateUser(user);
            return CreatedAtAction("GetUser", new { id = createdUser.Id }, createdUser);
        }

        [HttpPut("{id}")]
        //[Authorize]
        public async Task<IActionResult> UpdateUser(int id, User user)
        {
            if (id != user.Id)
                return BadRequest();

            var result = await _userRepository.UpdateUser(user);
            if (!result)
                return NotFound();

            return NoContent();
        }

        [HttpDelete("{id}")]
        //[Authorize]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var result = await _userRepository.DeleteUser(id);
            if (!result)
                return NotFound();

            return NoContent();
        }

        [HttpGet("email/{email}")]
        //[Authorize]
        public async Task<ActionResult<User>> GetUserByEmail(string email)
        {
            var result = await _userRepository.GetUserByEmail(email);
            if (result == null)
                return NotFound("User not found.");

            return Ok(result);
        }
    }
}
