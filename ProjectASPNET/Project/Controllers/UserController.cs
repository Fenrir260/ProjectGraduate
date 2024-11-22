using Microsoft.AspNetCore.Mvc;
using Project.Repositories;
using Project.Models;

namespace Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly IUsersRepository _usersRepository;

        public UserController(IUsersRepository usersRepository)
        {
            _usersRepository = usersRepository;
        }

        [HttpPost("createUser")]
        public async Task<ActionResult<Users>> CreateUserAsync(Users user)
        {
            await _usersRepository.CreateUserAsync(user);
            return Ok(user);
        }

        [HttpGet("getAllUsers")]
        public async Task<ActionResult<IEnumerable<Users>>> GetAllUserSAsync()
        {
            var allUsers = await _usersRepository.GetAllUsersAsync();
            return Ok(allUsers);
        }

        [HttpGet("{id}/getUserById")]
        public async Task<ActionResult<Users?>> GetUserByIdAsync(int id)
        {
            var user = await _usersRepository.GetUserByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }

        [HttpGet("{login}/getUserByLogin")]
        public async Task<ActionResult<Users?>> GetUserByLoginAsync(string login)
        {
            var user = await _usersRepository.GetUserByLoginAsync(login);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }

        [HttpPut("{id}/updateUser")]
        public async Task<ActionResult<Users>> UpdateUserAsync(int id, Users user)
        {
            if (id != user.Id)
            {
                return BadRequest();
            }
            await _usersRepository.UpdateUserAsync(user);
            return Ok(user);
        }

        [HttpDelete("{id}/deleteUserById")]
        public async Task<ActionResult> DeleteUserByIdAsync(int id)
        {
            await _usersRepository.DeleteUserByIdAsync(id);
            return NoContent();
        }

        [HttpDelete ("{login}/deleteUserByLogin")]
        public async Task<ActionResult> DeleteUserByLoginAsync(string login)
        {
            await _usersRepository.DeleteUserByLoginAsync (login);
            return NoContent();
        }

        [HttpPost("{login}/{password}/verify")]
        public async Task<ActionResult> Verify(string login, string password)
        {
            Console.WriteLine($"login {login} password {password} \n\n\n");

            if (await _usersRepository.VerifyPasswordAsync(login, password))
            {
                return Ok();
            }

            return BadRequest(); 
        }

        
    }
}
