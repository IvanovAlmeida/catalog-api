using Catalog.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Catalog.Data.Mappings
{
    public class UserMapping : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(p => p.Id);
            builder
                .Property(p => p.Name)
                .HasColumnType("VARCHAR(120)")
                .IsRequired(true);

            builder
                .Property(p => p.Username)
                .HasColumnType("VARCHAR(50)")
                .IsRequired(true);

            builder
                .Property(p => p.Email)
                .HasColumnType("VARCHAR(80)")
                .IsRequired(true);

            builder
                .Property(p => p.Password)
                .HasColumnType("VARCHAR(255)")
                .IsRequired(true);

            builder
                .Property(p => p.CreatedAt)
                .HasColumnType("datetime")
                .HasDefaultValueSql("GETDATE()")
                .ValueGeneratedOnAdd();

            builder
                .Property(p => p.DisabledAt)
                .HasColumnType("datetime")
                .IsRequired(false);
        }
    }
}