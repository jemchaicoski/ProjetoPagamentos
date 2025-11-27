using ProjetoPagamentos.Domain.Enums;

namespace ProjetoPagamentos.Domain.Entities.Transactions
{
    public class CaptureTransaction : BaseTransaction
    {
        public CaptureTransaction(Guid accountId, decimal amount, string referenceId, Currency currency) 
            : base(accountId, TransactionType.Capture, amount, referenceId, currency) {}

        public override bool ValidateTransaction(string errorMensage)
        {
            if (this.Amount <= 0 || errorMensage != "")
            {
                this.MarkAsFailed(errorMensage);
                return false;
            }
            this.MarkAsCompleted();
            return true;
        }
    }
}
