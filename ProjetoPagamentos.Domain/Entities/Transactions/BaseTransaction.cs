using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using ProjetoPagamentos.Domain.Enums;

namespace ProjetoPagamentos.Domain.Entities.Transactions
{
    public abstract class BaseTransaction
    {
        protected BaseTransaction(Guid accountId, TransactionType transactionType, decimal amount)
        {
            AccountId = accountId;
            TransactionType = transactionType;
            Amount = amount;
            CreatedAt = DateTime.UtcNow;
            TransactionStatus = TransactionStatus.Pending;
        }

        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string TransactionId { get; set; } = ObjectId.GenerateNewId().ToString();

        [BsonRepresentation(BsonType.String)]
        public Guid AccountId { get; set; }
        public decimal Amount { get; set; }
        public Currency Currency { get; set; }
        public string ReferenceId { get; set; }
        public Dictionary<string, string> Metadata { get; set; }
        public TransactionType TransactionType { get; set; }
        public DateTime CreatedAt { get; set; }
        public TransactionStatus TransactionStatus { get; set; }
        public string? ErrorMessage { get; set; }

        abstract public bool ValidateTransaction();

        protected void MarkAsCompleted()
        {
            TransactionStatus = TransactionStatus.Success;
            ErrorMessage = null;
        }

        protected void MarkAsPendingt()
        {
            TransactionStatus = TransactionStatus.Pending;
            ErrorMessage = null;
        }

        protected void MarkAsFailed(string errorMessage)
        {
            TransactionStatus = TransactionStatus.Failed;
            ErrorMessage = errorMessage;
        }
    }
}
