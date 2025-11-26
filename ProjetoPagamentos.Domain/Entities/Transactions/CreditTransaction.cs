using ProjetoPagamentos.Domain.Enums;

namespace ProjetoPagamentos.Domain.Entities.Transactions
{
    public class CreditTransaction : BaseTransaction
    {
        public CreditTransaction(Guid accountId, decimal amount, string referenceId)
            : base(accountId, TransactionType.Credit, amount, referenceId)
        {
        }

        public override bool ValidateTransaction()
        {
            if (this.Amount <= 0) {
                this.MarkAsFailed("Valor de crédito não pode ser menor ou igual a 0");
                return false;
            }
            return true;
        }
    }
}
