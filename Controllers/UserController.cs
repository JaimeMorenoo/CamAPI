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
        [HttpPut("update")]
        public async Task<IActionResult> UpdateUser([FromBody] User model)
        {
            // Lógica para buscar y actualizar el usuario en la base de datos usando el modelo proporcionado
            // Actualiza los campos pertinentes con la información proporcionada en el modelo
            // Retorna un mensaje de éxito o fallo apropiado

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
    }
}
