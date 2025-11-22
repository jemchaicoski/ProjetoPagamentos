using ProjetoPagamentos.Domain.ValueObjects;
using ProjetoPagamentos.Domain.Enums;
using Xunit;

namespace ProjetoPagamentos.Tests.Domain.ValueObjects
{
    public class UserDocumentTests
    {
        private const string VALID_CPF = "18792311075";
        private const string VALID_CPF_FORMATTED = "187.923.110-75";
        private const string VALID_CNPJ = "44717171000101";
        private const string VALID_CNPJ_FORMATTED = "44.717.171/0001-01";
        private const string INVALID_CPF = "18792311000";
        private const string INVALID_CNPJ = "44717171000100";
        private const string SHORT_DOCUMENT = "123";
        private const string LONG_DOCUMENT = "123456789012345";

        [Fact]
        public void Constructor_WithValidCpf_ShouldInitializeCorrectly()
        {
            var userDocument = new UserDocument(VALID_CPF);

            Assert.Equal(VALID_CPF, userDocument.Value);
            Assert.Equal(DocumentType.Cpf, userDocument.Type);
        }

        [Fact]
        public void Constructor_WithValidFormattedCpf_ShouldInitializeCorrectly()
        {
            var userDocument = new UserDocument(VALID_CPF_FORMATTED);

            Assert.Equal(VALID_CPF, userDocument.Value);
            Assert.Equal(DocumentType.Cpf, userDocument.Type);
        }

        [Fact]
        public void Constructor_WithValidCnpj_ShouldInitializeCorrectly()
        {
            var userDocument = new UserDocument(VALID_CNPJ);

            Assert.Equal(VALID_CNPJ, userDocument.Value);
            Assert.Equal(DocumentType.Cnpj, userDocument.Type);
        }

        [Fact]
        public void Constructor_WithValidFormattedCnpj_ShouldInitializeCorrectly()
        {
            var userDocument = new UserDocument(VALID_CNPJ_FORMATTED);

            Assert.Equal(VALID_CNPJ, userDocument.Value);
            Assert.Equal(DocumentType.Cnpj, userDocument.Type);
        }

        [Fact]
        public void Constructor_WithInvalidCpf_ShouldThrowArgumentException()
        {
            Assert.Throws<ArgumentException>(() => new UserDocument(INVALID_CPF));
        }

        [Fact]
        public void Constructor_WithInvalidCnpj_ShouldThrowArgumentException()
        {
            Assert.Throws<ArgumentException>(() => new UserDocument(INVALID_CNPJ));
        }

        [Fact]
        public void Constructor_WithShortDocument_ShouldThrowArgumentException()
        {
            Assert.Throws<ArgumentException>(() => new UserDocument(SHORT_DOCUMENT));
        }

        [Fact]
        public void Constructor_WithLongDocument_ShouldThrowArgumentException()
        {
            Assert.Throws<ArgumentException>(() => new UserDocument(LONG_DOCUMENT));
        }

        [Fact]
        public void Constructor_WithEmptyString_ShouldThrowArgumentException()
        {
            Assert.Throws<ArgumentException>(() => new UserDocument(""));
        }

        [Fact]
        public void Constructor_WithWhitespace_ShouldThrowArgumentException()
        {
            Assert.Throws<ArgumentException>(() => new UserDocument("   "));
        }

        [Theory]
        [InlineData("187.923.110-75", "18792311075")]
        [InlineData("44.717.171/0001-01", "44717171000101")]
        [InlineData("187-923-110-75", "18792311075")]
        [InlineData("44/717/171/0001-01", "44717171000101")]
        public void Constructor_WithFormattedDocuments_ShouldRemoveFormatting(string input, string expected)
        {
            var userDocument = new UserDocument(input);

            Assert.Equal(expected, userDocument.Value);
        }
    }
}