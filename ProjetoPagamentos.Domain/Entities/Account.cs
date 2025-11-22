using ProjetoPagamentos.Domain.Enums;
using ProjetoPagamentos.Entities;

namespace ProjetoPagamentos.Domain.Entities
{
    public class Account : BaseEntity
    {
        public Account(Guid userId)
        {
            if (userId == Guid.Empty)
                throw new ArgumentException("UserId cannot be empty", nameof(userId));

            UserId = userId;
            AccountStatus = AccountStatus.Active;
        }

        public decimal AvailableBalance { get; set; }
        public decimal ReservedBalance { get; set; }
        public decimal CreditLimit { get; set; }
        public AccountStatus AccountStatus { get; set; }

        public Guid UserId { get; set; }
        public User User { get; set; } = null!;
    }
}
