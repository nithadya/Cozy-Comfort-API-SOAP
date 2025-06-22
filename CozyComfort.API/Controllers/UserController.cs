using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CozyComfort.API.Data;
using CozyComfort.API.Models;

namespace CozyComfort.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly CozyComfortDbContext _context;

        public UserController(CozyComfortDbContext context)
        {
            _context = context;
        }

        [HttpPost("authenticate")]
        public async Task<ActionResult<User>> AuthenticateUser(string username, string password, UserType userType)
        {
            try
            {
                var user = await _context.Users
                    .FirstOrDefaultAsync(u => u.Username == username && u.PasswordHash == password && u.UserType == userType);
                
                if (user != null)
                {
                    return Ok(user);
                }
                return NotFound("Invalid credentials");
            }
            catch (Exception ex)
            {
                return BadRequest($"Error authenticating user: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<ActionResult<User>> CreateUser(User user)
        {
            try
            {
                _context.Users.Add(user);
                await _context.SaveChangesAsync();
                return CreatedAtAction(nameof(GetUser), new { id = user.UserId }, user);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error creating user: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(int id)
        {
            try
            {
                var user = await _context.Users.FindAsync(id);
                if (user == null)
                {
                    return NotFound();
                }
                return Ok(user);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error retrieving user: {ex.Message}");
            }
        }

        [HttpGet]
        public async Task<ActionResult<List<User>>> GetAllUsers()
        {
            try
            {
                var users = await _context.Users.ToListAsync();
                return Ok(users);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error retrieving users: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(int id, User user)
        {
            try
            {
                if (id != user.UserId)
                {
                    return BadRequest("User ID mismatch");
                }

                _context.Entry(user).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return Ok(user);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error updating user: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            try
            {
                var user = await _context.Users.FindAsync(id);
                if (user == null)
                {
                    return NotFound();
                }

                _context.Users.Remove(user);
                await _context.SaveChangesAsync();
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest($"Error deleting user: {ex.Message}");
            }
        }

        [HttpGet("bytype/{userType}")]
        public async Task<ActionResult<List<User>>> GetUsersByType(UserType userType)
        {
            try
            {
                var users = await _context.Users
                    .Where(u => u.UserType == userType)
                    .ToListAsync();
                return Ok(users);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error retrieving users by type: {ex.Message}");
            }
        }
    }
} 