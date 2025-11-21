using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProjetoPagamentos.Domain.Entities;

namespace ProjetoPagamentos.Infrastructure.Persistence.Configuration
{
    public class AccountConfiguration : IEntityTypeConfiguration<Account>
    {
        public void Configure(EntityTypeBuilder<Account> builder)
        {
            builder.ToTable("Accounts");

            builder.HasKey(a => a.Id);

            builder.Property(a => a.AvailableBalance)
                .HasColumnType("decimal(18,2)")
                .IsRequired();

            builder.Property(a => a.ReservedBalance)
                .HasColumnType("decimal(18,2)")
                .IsRequired();

            builder.Property(a => a.CreditLimit)
                .HasColumnType("decimal(18,2)")
                .IsRequired();

            builder.Property(a => a.AccountStatus)
                .HasConversion<int>()
                .IsRequired();

            builder.HasOne(a => a.User)
               .WithMany(u => u.Accounts)
               .HasForeignKey(a => a.UserId);
        }
    }
}
