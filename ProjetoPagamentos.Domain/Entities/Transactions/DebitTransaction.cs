using ProjetoPagamentos.Domain.Enums;

namespace ProjetoPagamentos.Domain.Entities.Transactions
{
    public class DebitTransaction : BaseTransaction
    {
        public DebitTransaction(Guid accountId, decimal amount, string referenceId, Currency currency)
            : base(accountId, TransactionType.Debit, amount, referenceId, currency) { }

        public override bool ValidateTransaction(string errorMensage)
        {
            //TODO: adicionar validação de saldo negativo na conta
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
