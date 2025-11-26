namespace ProjetoPagamentos.Api.Models.Requests
{
    public class CreateCreditTransactionRequest
    {
        public required Guid AccountId { get; set; }
        public required decimal Amount { get; set; }
        public required string ReferenceId { get; set; }
    }
}
