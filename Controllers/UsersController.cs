using Microsoft.AspNetCore.Mvc;
using UserManagementAPI.Models;

namespace UserManagementAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private static List<User> _users = new List<User>
        {
            new User { Id = 1, FirstName = "John", LastName="Doe", DateOfBirth=new DateTime(1990, 03, 12), Email = "john@example.com" },
            new User { Id = 2, FirstName = "Jane", LastName="Doe", DateOfBirth=new DateTime(2000, 03, 12), Email = "jane@example.com" }
        };

        /// <summary>
        /// Get all users.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult<IEnumerable<User>> GetAll()
        {
            try
            {
                return Ok(_users);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        /// <summary>
        /// Get user by id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public ActionResult<User> Get(int id)
        {
            try
            {
                var user = _users.FirstOrDefault(u => u.Id == id);
                return user == null ? NotFound() : Ok(user);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        /// <summary>
        /// Create new user. 
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult<User> Post(User user)
        {
            try
            {
                user.Id = _users.Max(u => u.Id) + 1;

                if (_users.Any(u => u.FirstName == user.FirstName && u.MiddleName == user.MiddleName && u.LastName == user.LastName && u.DateOfBirth == user.DateOfBirth))
                    return BadRequest("User already exists in the system");

                _users.Add(user);
                return CreatedAtAction(nameof(Get), new { id = user.Id }, user);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        /// <summary>
        /// Update a user by id.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="updatedUser"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public IActionResult Put(int id, User updatedUser)
        {
            try
            {
                var user = _users.FirstOrDefault(u => u.Id == id);
                if (user == null) return NotFound();

                user.FirstName = updatedUser.FirstName;
                user.LastName = updatedUser.LastName;
                user.MiddleName = updatedUser.MiddleName;
                user.DateOfBirth = updatedUser.DateOfBirth;
                user.Email = updatedUser.Email;

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        /// <summary>
        /// Delete a user.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                var user = _users.FirstOrDefault(u => u.Id == id);
                if (user == null) return NotFound();

                _users.Remove(user);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}