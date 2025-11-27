namespace ProjetoPagamentos.Domain.Models.Requests
{
    public class CreateDebitTransactionRequest
    {
        public Guid AccountId { get; set; }
        public decimal Amount { get; set; }
        public int Currency { get; set; }
        public string ReferenceId { get; set; }
    }
}
