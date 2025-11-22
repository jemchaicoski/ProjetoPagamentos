namespace ProjetoPagamentos.Api.Models.Requests
{
    public class CreateAccountRequest
    {
        public Guid UserId { get; set; }
        public decimal creditValue { get; set; }
    }
}
