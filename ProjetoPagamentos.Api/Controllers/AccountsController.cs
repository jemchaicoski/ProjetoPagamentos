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
