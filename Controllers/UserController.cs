using Camp.Models;
using Camp.Services;
using Microsoft.AspNetCore.Mvc;

namespace Camp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> CreateUser([FromBody] User newUser)
        {
            if (newUser == null)
            {
                return BadRequest("User is null.");
            }

            try
            {
                var createdUser = await _userService.CreateUserAsync(newUser);
                return Ok(createdUser);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }



        [HttpPut("update")]
        public async Task<IActionResult> UpdateUser([FromBody] User model)
        {

           var result = await _userService.UpdateUserAsync(model);

            // Verifica si la actualización fue exitosa
            if (result)
            {
                return Ok(new { message = "User information updated successfully." });
            }
            else
            {
                return BadRequest(new { message = "Failed to update user information." });
            }
        }

        [HttpGet("{id}")]
        public ActionResult<User> GetUserById(int id)
        {
            // Retrieve user by ID from the service
            var user = _userService.GetUserById(id);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }
    }
}
