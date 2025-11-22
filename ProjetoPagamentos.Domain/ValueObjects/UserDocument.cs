using Maoli;
using ProjetoPagamentos.Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace ProjetoPagamentos.Domain.ValueObjects
{
    [Owned]
    public sealed class UserDocument
    {
        public string Document { get; }
        public DocumentType Type { get; }

        public UserDocument(string document)
        {
            document = new string(document.Where(char.IsDigit).ToArray());

            if (document.Length == 11)
            {
                Type = DocumentType.Cpf;
                if (!Cpf.Validate(document)) throw new ArgumentException("CPF inválido");
            }
            else if (document.Length == 14)
            {
                Type = DocumentType.Cnpj;
                if (!Cnpj.Validate(document)) throw new ArgumentException("CNPJ inválido");
            }
            else
            {
                throw new ArgumentException("Documento deve ter 11 (CPF) ou 14 (CNPJ) dígitos.");
            }

            Document = document;
        }
    }
}
