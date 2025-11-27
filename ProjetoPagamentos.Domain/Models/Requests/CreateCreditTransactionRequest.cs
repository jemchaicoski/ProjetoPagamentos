namespace ProjetoPagamentos.Api.Models.Requests
{
    public class CreateCreditTransactionRequest
    {
        public Guid AccountId { get; set; }
        public decimal Amount { get; set; }
        public int Currency { get; set; }
        public string ReferenceId { get; set; }
    }
}
