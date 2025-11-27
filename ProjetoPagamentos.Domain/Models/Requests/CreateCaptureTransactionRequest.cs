namespace ProjetoPagamentos.Domain.Models.Requests
{
    public class CreateCaptureTransactionRequest
    {
        public Guid AccountId { get; set; }
        public decimal Amount { get; set; }
        public int Currency { get; set; }
        public string ReferenceId { get; set; }
    }
}
