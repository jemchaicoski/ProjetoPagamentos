namespace ProjetoPagamentos.Domain.Models.Requests
{
    public class CreateTransferTransactionRequest
    {
        public Guid HolderAccountId { get; set; }
        public Guid TargetAccountId { get; set; }
        public decimal Amount { get; set; }
        public int Currency { get; set; }
        public string ReferenceId { get; set; }
    }
}
