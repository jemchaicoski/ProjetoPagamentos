using ProjetoPagamentos.Domain.ValueObjects;
using ProjetoPagamentos.Entities;

namespace ProjetoPagamentos.Domain.Entities
{
    public class User : BaseEntity
    {
        public User(Document document)
        {
            Document = document;
        }
        public required Document Document { get; set; }
        public bool IsDeleted { get; set; } = false;

        public ICollection<Account> Accounts { get; set; } = new List<Account>();
    }
}
