using ProjetoPagamentos.Domain.Enums;
using ProjetoPagamentos.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetoPagamentos.Domain.Entities
{
    public class Account : BaseEntity
    {
        public decimal AvailableBalance { get; set; }
        public decimal ReservedBalance { get; set; }
        public decimal CreditLimit { get; set; }
        public AccountStatus AccountStatus { get; set; }
    }
}
