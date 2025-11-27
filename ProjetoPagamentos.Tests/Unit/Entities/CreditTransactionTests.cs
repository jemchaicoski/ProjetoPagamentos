using ProjetoPagamentos.Domain.Entities.Transactions;
using ProjetoPagamentos.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ProjetoPagamentos.Tests.Unit.Entities
{
    public class CreditTransactionTests
    {
        [Fact]
        public void ValidateTransaction_ShouldReturnFalse_WhenAmountIsZero()
        {
            var tx = new CreditTransaction(Guid.NewGuid(), 0, "ref123", Currency.BRL);

            var result = tx.ValidateTransaction("");

            Assert.False(result);
            Assert.Equal(TransactionStatus.Failed, tx.TransactionStatus);
        }

        [Fact]
        public void ValidateTransaction_ShouldReturnFalse_WhenErrorMessageProvided()
        {
            var tx = new CreditTransaction(Guid.NewGuid(), 100, "ref123", Currency.BRL);

            var result = tx.ValidateTransaction("erro");

            Assert.False(result);
            Assert.Equal(TransactionStatus.Failed, tx.TransactionStatus);
            Assert.Equal("erro", tx.ErrorMessage);
        }

        [Fact]
        public void ValidateTransaction_ShouldReturnTrue_WhenValid()
        {
            var tx = new CreditTransaction(Guid.NewGuid(), 150, "ref123", Currency.USD);

            var result = tx.ValidateTransaction("");

            Assert.True(result);
            Assert.Equal(TransactionStatus.Success, tx.TransactionStatus);
            Assert.Null(tx.ErrorMessage);
        }
    }
}
