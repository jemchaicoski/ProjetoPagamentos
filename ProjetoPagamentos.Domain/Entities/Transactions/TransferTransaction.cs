using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using ProjetoPagamentos.Domain.Enums;

namespace ProjetoPagamentos.Domain.Entities.Transactions
{
    public class TransferTransaction : BaseTransaction
    {
        public TransferTransaction(Guid accountId, Guid targetAccountId, decimal amount, string referenceId, Currency currency) 
            : base(accountId, TransactionType.Transfer, amount, referenceId, currency) 
        {
            TargetAccountId = targetAccountId;
        }

        [BsonRepresentation(BsonType.String)]
        public Guid TargetAccountId { get; set; }

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
