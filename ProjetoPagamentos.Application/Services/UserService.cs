using ProjetoPagamentos.Application.Services.Interfaces;
using ProjetoPagamentos.Domain.Entities;
using ProjetoPagamentos.Domain.Models.Requests;
using ProjetoPagamentos.Domain.Models.Responses;
using ProjetoPagamentos.Domain.ValueObjects;

namespace ProjetoPagamentos.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<CreateUserResponse> ProcessCreateUserAsync(CreateUserRequest request)
        {
            if (await _userRepository.UserExistsAsync(CleanDocument(request.UserDocument!)))
                throw new Exception("Usuário já existe");

            var document = new UserDocument(request.UserDocument!);
            var user = new User { UserDocument = document };

            await _userRepository.AddAsync(user);

            var response = new CreateUserResponse
            {
                Id = user.Id,
                UserDocument = user.UserDocument.Document,
                DocumentType = user.UserDocument.Type.ToString()
            };

            return response;
        }
        private string CleanDocument(string document)
        {
            if (string.IsNullOrWhiteSpace(document))
                throw new ArgumentException("Documento não pode ser vazio");

            return new string(document.Where(char.IsDigit).ToArray());
        }
    }
}
