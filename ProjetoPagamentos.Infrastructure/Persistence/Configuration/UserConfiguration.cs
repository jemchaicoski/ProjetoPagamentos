using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProjetoPagamentos.Domain.Entities;

namespace ProjetoPagamentos.Infrastructure.Persistence.Configuration
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("Users");

            builder.HasKey(u => u.Id);
            builder.OwnsOne(u => u.Document, doc =>
            {
                doc.Property(d => d.Value)
                    .HasColumnName("Document")
                    .IsRequired();
            });

            builder.Navigation(u => u.Document).IsRequired();

            builder.Property(u => u.IsDeleted)
                .IsRequired()
                .HasDefaultValue(false);

            builder.HasMany(u => u.Accounts)
               .WithOne(a => a.User)
               .HasForeignKey(a => a.UserId)
               .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
