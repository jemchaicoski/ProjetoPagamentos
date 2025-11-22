using ProjetoPagamentos.Domain.Entities;
using ProjetoPagamentos.Domain.Enums;
using ProjetoPagamentos.Entities;
using Xunit;

namespace ProjetoPagamentos.Tests.Unit.Entities
{
    public class AccountTests
    {
        [Fact]
        public void Constructor_WithValidUserId_ShouldInitializeCorrectly()
        {
            var userId = Guid.NewGuid();

            var account = new Account(userId);

            Assert.Equal(userId, account.UserId);
            Assert.Equal(AccountStatus.Active, account.AccountStatus);
            Assert.Equal(0, account.AvailableBalance);
            Assert.Equal(0, account.ReservedBalance);
            Assert.Equal(0, account.CreditLimit);
        }

        [Fact]
        public void Constructor_WithEmptyUserId_ShouldThrowException()
        {
            var emptyUserId = Guid.Empty;

            Assert.Throws<ArgumentException>(() => new Account(emptyUserId));
        }

        [Theory]
        [InlineData(100.50)]
        [InlineData(0)]
        [InlineData(9999.99)]
        public void AvailableBalance_ShouldSetAndGetCorrectly(decimal balance)
        {
            var account = new Account(Guid.NewGuid());

            account.AvailableBalance = balance;

            Assert.Equal(balance, account.AvailableBalance);
        }

        [Theory]
        [InlineData(50.25)]
        [InlineData(0)]
        [InlineData(500.75)]
        public void ReservedBalance_ShouldSetAndGetCorrectly(decimal reservedBalance)
        {
            var account = new Account(Guid.NewGuid());

            account.ReservedBalance = reservedBalance;

            Assert.Equal(reservedBalance, account.ReservedBalance);
        }

        [Theory]
        [InlineData(1000)]
        [InlineData(0)]
        [InlineData(5000)]
        public void CreditLimit_ShouldSetAndGetCorrectly(decimal creditLimit)
        {
            var account = new Account(Guid.NewGuid());

            account.CreditLimit = creditLimit;

            Assert.Equal(creditLimit, account.CreditLimit);
        }

        [Theory]
        [InlineData(AccountStatus.Active)]
        [InlineData(AccountStatus.Blocked)]
        [InlineData(AccountStatus.Inactive)]
        public void AccountStatus_ShouldSetAndGetCorrectly(AccountStatus status)
        {
            var account = new Account(Guid.NewGuid());

            account.AccountStatus = status;

            Assert.Equal(status, account.AccountStatus);
        }

        [Fact]
        public void TotalBalance_ShouldReturnSumOfAvailableAndReservedBalance()
        {
            var account = new Account(Guid.NewGuid())
            {
                AvailableBalance = 1000,
                ReservedBalance = 500
            };

            var totalBalance = account.AvailableBalance + account.ReservedBalance;

            Assert.Equal(1500, totalBalance);
        }

        [Fact]
        public void Account_InitialState_ShouldHaveZeroBalances()
        {
            var account = new Account(Guid.NewGuid());

            Assert.Equal(0, account.AvailableBalance);
            Assert.Equal(0, account.ReservedBalance);
            Assert.Equal(0, account.CreditLimit);
        }

        [Fact]
        public void Account_ShouldInheritFromBaseEntity()
        {
            var account = new Account(Guid.NewGuid());

            Assert.IsAssignableFrom<BaseEntity>(account);
        }
    }
}