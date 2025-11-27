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

        /// <summary>
        /// Cria um novo usuário no sistema.
        /// </summary>
        /// <param name="request">
        /// Objeto contendo os dados necessários para criar o usuário:
        /// - <b>UserDocument</b>: Documento único do usuário (CPF, CNPJ, etc).
        /// </param>
        /// <returns>Id do usuário criado.</returns>
        /// <response code="201">Usuário criado com sucesso.</response>
        /// <response code="400">Erro de validação ou documento inválido.</response>
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

