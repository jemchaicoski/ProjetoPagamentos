using Maoli;
using ProjetoPagamentos.Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace ProjetoPagamentos.Domain.ValueObjects
{
    [Owned]
    public sealed class Document
    {
        public string Value { get; }
        public DocumentType Type { get; }

        public Document(string value)
        {
            value = new string(value.Where(char.IsDigit).ToArray());

            if (value.Length == 11)
            {
                Type = DocumentType.Cpf;
                if (!Cpf.Validate(value)) throw new ArgumentException("CPF inválido");
            }
            else if (value.Length == 14)
            {
                Type = DocumentType.Cnpj;
                if (!Cnpj.Validate(value)) throw new ArgumentException("CNPJ inválido");
            }
            else
            {
                throw new ArgumentException("Documento deve ter 11 (CPF) ou 14 (CNPJ) dígitos.");
            }

            Value = value;
        }
    }
}
