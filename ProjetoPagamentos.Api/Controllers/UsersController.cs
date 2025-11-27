using Microsoft.AspNetCore.Mvc;
using ProjetoPagamentos.Application.Services.Interfaces;
using ProjetoPagamentos.Domain.Models.Requests;

namespace ProjetoPagamentos.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] CreateUserRequest request)
        {
            try
            {
                var response = _userService.ProcessCreateUserAsync(request).Result;

                return CreatedAtAction(nameof(CreateUser), new { id = response.Id }, response);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}

