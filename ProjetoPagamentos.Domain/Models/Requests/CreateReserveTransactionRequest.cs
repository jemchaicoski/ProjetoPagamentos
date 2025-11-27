using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetoPagamentos.Domain.Models.Requests
{
    public class CreateReserveTransactionRequest
    {
        public Guid AccountId { get; set; }
        public decimal Amount { get; set; }
        public int Currency { get; set; }
        public string ReferenceId { get; set; }
    }
}
