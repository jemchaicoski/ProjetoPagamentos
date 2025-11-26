using ProjetoPagamentos.Domain.Entities;
using ProjetoPagamentos.Domain.ValueObjects;
using Xunit;

namespace ProjetoPagamentos.Tests.Unit.Entities
{
    public class UserTests
    {
        private const string VALID_CNPJ = "58.983.928/0001-13";
        private const string VALID_CPF = "748.842.240-60";

        [Fact]
        public void Constructor_WithValidCPF_ShouldInitializeCorrectly()
        {
            var document = new UserDocument(VALID_CPF);

            var user = new User { UserDocument = document};

            Assert.Equal(document, user.UserDocument);
            Assert.False(user.IsDeleted);
            Assert.NotNull(user.Accounts);
            Assert.Empty(user.Accounts);
        }

        public void Constructor_WithValidCNPJ_ShouldInitializeCorrectly()
        {
            var document = new UserDocument(VALID_CPF);

            var user = new User { UserDocument = document };

            Assert.Equal(document, user.UserDocument);
            Assert.False(user.IsDeleted);
            Assert.NotNull(user.Accounts);
            Assert.Empty(user.Accounts);
        }

        [Fact]
        public void IsDeleted_ShouldSetAndGetCorrectly()
        {
            var document = new UserDocument(VALID_CNPJ);
            var user = new User { UserDocument = document };

            user.IsDeleted = true;

            Assert.True(user.IsDeleted);
        }

        [Fact]
        public void Accounts_ShouldInitializeAsEmptyCollection()
        {
            var document = new UserDocument(VALID_CNPJ);
            var user = new User { UserDocument = document };

            Assert.NotNull(user.Accounts);
            Assert.Empty(user.Accounts);
        }

        [Fact]
        public void PrivateConstructor_ShouldCreateInstance()
        {
            var user = Activator.CreateInstance<User>();

            Assert.NotNull(user);
            Assert.NotNull(user.Accounts);
        }
    }
}