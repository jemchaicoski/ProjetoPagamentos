using ProjetoPagamentos.Domain.ValueObjects;
using ProjetoPagamentos.Entities;

namespace ProjetoPagamentos.Domain.Entities
{
    public class User : BaseEntity
    {
        public required Document Document { get; set; }
        public bool IsDeleted { get; set; } = false;
    }
}
