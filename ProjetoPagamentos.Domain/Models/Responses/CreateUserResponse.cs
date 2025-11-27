namespace ProjetoPagamentos.Domain.Models.Responses
{
    public class CreateUserResponse
    {
        public Guid Id { get; set; }
        public string? UserDocument { get; set; }
        public string? DocumentType { get; set; }
    }
}
