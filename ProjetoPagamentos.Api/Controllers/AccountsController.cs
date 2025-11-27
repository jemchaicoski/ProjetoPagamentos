using Microsoft.AspNetCore.Mvc;
using ProjetoPagamentos.Application.Services.Interfaces;
using ProjetoPagamentos.Domain.Models.Requests;

namespace ProjetoPagamentos.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountsController : ControllerBase
    {
        private readonly IAccountService _accountService;

        public AccountsController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        /// <summary>
        /// Cria uma nova conta vinculada a um usuário fazendo 1 depósito inicial.
        /// </summary>
        /// <param name="request">
        /// Objeto contendo os dados necessários para criar a conta:
        /// - <b>UserId</b>: Identificador do usuário.
        /// - <b>Amount</b>: Valor inicial da conta.
        /// - <b>Currency</b>: Código numérico da moeda utilizada (ex.: 0 = BRL, 1 = USD).
        /// - <b>ReferenceId</b>: Identificador externo para idempotência.
        /// </param>
        /// <returns>Id da conta criada.</returns>
        /// <response code="201">Conta criada com sucesso.</response>
        /// <response code="400">Erro de validação ou dados inválidos.</response>
        [HttpPost]
        public async Task<IActionResult> CreateAccount([FromBody] CreateAccountRequest request)
        {
            try {

                var response = _accountService.ProcessCreateAccountAsync(request).Result;

                return CreatedAtAction(nameof(CreateAccount), new { id = response.Id }, response);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }

}
