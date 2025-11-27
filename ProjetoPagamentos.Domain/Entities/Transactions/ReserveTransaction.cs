using ProjetoPagamentos.Domain.Enums;

namespace ProjetoPagamentos.Domain.Entities.Transactions
{
    public class ReserveTransaction : BaseTransaction
    {
        public ReserveTransaction(Guid accountId, decimal amount, string referenceId, Currency currency) 
            : base(accountId, TransactionType.Reserve, amount, referenceId, currency) { }

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
