namespace ProjetoPagamentos.Api.Models.Requests
{
    public class CreateAccountRequest
    {
        public required Guid UserId { get; set; }
        public required decimal Amount { get; set; }
        public required int Currency { get; set; }
        public required string ReferenceId { get; set; }
    }
}
