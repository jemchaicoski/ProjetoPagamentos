using Microsoft.AspNetCore.Mvc;
using ProjetoPagamentos.Domain.Entities;
using ProjetoPagamentos.Domain.ValueObjects;

namespace ProjetoPagamentos.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUserRepository _userRepository;

        public UsersController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] CreateUserRequest request)
        {
            try
            {
                if (await _userRepository.UserExistsAsync(CleanDocument(request.UserDocument!)))
                    return BadRequest("Documento já existe");

                var document = new UserDocument(request.UserDocument!);
                var user = new User { UserDocument = document };

                await _userRepository.AddAsync(user);

                var response = new CreateUserResponse
                {
                    Id = user.Id,
                    UserDocument = user.UserDocument.Document,
                    DocumentType = user.UserDocument.Type.ToString()
                };

                return CreatedAtAction(nameof(GetUser), new { id = user.Id }, response);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUser(Guid id)
        {
            var user = await _userRepository.GetByIdAsync(id);
            if (user == null) return NotFound();

            var response = new CreateUserResponse
            {
                Id = user.Id,
                UserDocument = user.UserDocument.Document,
                DocumentType = user.UserDocument.Type.ToString()
            };

            return Ok(response);
        }

        private string CleanDocument(string document)
        {
            if (string.IsNullOrWhiteSpace(document))
                throw new ArgumentException("Documento não pode ser vazio");

            return new string(document.Where(char.IsDigit).ToArray());
        }
    }

    public class CreateUserRequest
    {
        public string? UserDocument { get; set; }
    }

    public class CreateUserResponse
    {
        public Guid Id { get; set; }
        public string? UserDocument { get; set; }
        public string? DocumentType { get; set; }
    }
}

