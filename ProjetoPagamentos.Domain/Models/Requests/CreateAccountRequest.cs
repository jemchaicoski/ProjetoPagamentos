namespace ProjetoPagamentos.Api.Models.Requests
{
    public class CreateAccountRequest
    {
        public Guid UserId { get; set; }
        public decimal Amount { get; set; }
        public int Currency { get; set; }
        public string ReferenceId { get; set; }
    }
}
