namespace ProjetoPagamentos.Api.Models.Responses
{
    public class CreateCreditTransactionResponse
    {
        public string? TransactionId { get; set; }
        public bool? Success { get; set; }
        public string? ErrorMessage { get; set; }
    }
}
