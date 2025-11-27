namespace ProjetoPagamentos.Domain.Models.Responses
{
    public class CreateTransactionResponse
    {
        public string? TransactionId { get; set; }
        public bool? Success { get; set; }
        public string? ErrorMessage { get; set; }
    }
}
