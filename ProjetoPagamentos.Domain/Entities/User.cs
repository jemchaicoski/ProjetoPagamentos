using ProjetoPagamentos.Domain.ValueObjects;
using ProjetoPagamentos.Entities;

namespace ProjetoPagamentos.Domain.Entities
{
    public class User : BaseEntity
    {
        public User() { }

        public required UserDocument UserDocument { get; set; }
        public bool IsDeleted { get; set; } = false;

        public ICollection<Account> Accounts { get; set; } = new List<Account>();
    }
}
