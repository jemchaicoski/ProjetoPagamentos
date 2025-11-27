namespace ProjetoPagamentos.Domain.Models.Responses
{

    public class CreateAccountResponse
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public decimal AvailableBalance { get; set; }
        public decimal ReservedBalance { get; set; }
        public decimal CreditLimit { get; set; }
        public string AccountStatus { get; set; }
        public string? TransactionId { get; set; }
    }
}
