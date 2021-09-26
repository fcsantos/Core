using Core.Business.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Core.Data.Mappings
{
    public class ClientMapping : IEntityTypeConfiguration<Client>
    {
        public void Configure(EntityTypeBuilder<Client> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(p => p.Name)
                .IsRequired()
                .HasColumnType("varchar(250)");

            builder.Property(p => p.NIF)
                .IsRequired()
                .HasColumnType("char(14)");

            builder.Property(p => p.UserId)
                .IsRequired()
                .HasColumnType("varchar(MAX)");

            builder.Property(p => p.IsActive)
                .IsRequired(false)
                .HasDefaultValueSql("1")
                .HasColumnType("bit");

            builder.Property(p => p.Email)
                .IsRequired()
                .HasColumnType("varchar(250)");

            builder.Property(p => p.SecretKey)
                .IsRequired(false)
                .HasColumnType("varchar(MAX)");

            builder.Property(p => p.ApiKey)
                .IsRequired(false)
                .HasColumnType("varchar(MAX)");

            builder.Property(p => p.IVBase64)
                .IsRequired(false)
                .HasColumnType("varchar(MAX)");

            builder.Property(p => p.Certificate)
                .IsRequired(false)
                .HasColumnType("varchar(MAX)");

            builder.Property(p => p.CertificatePathPfx)
                .IsRequired()
                .HasColumnType("varchar(300)");

            builder.Property(p => p.PasswordPfx)
                .IsRequired()
                .HasColumnType("varchar(200)");

            builder.Property(p => p.CertificatePathCer)
                .IsRequired()
                .HasColumnType("varchar(300)");

            builder.Property(p => p.UsernameAT)
                .IsRequired()
                .HasColumnType("varchar(200)");

            builder.Property(p => p.PasswordAT)
                .IsRequired()
                .HasColumnType("varchar(200)");

            builder.Property(p => p.CreatedBy)
                .IsRequired(false)
                .HasColumnType("varchar(max)");

            builder.Property(p => p.CreatedDate)
                .IsRequired(false)
                .HasColumnType("DateTime")
                .HasDefaultValueSql("GETDATE()")
                .ValueGeneratedOnAdd();

            builder.Property(p => p.UpdatedBy)
                .IsRequired(false)
                .HasColumnType("varchar(max)");

            builder.Property(p => p.UpdatedDate)
                .IsRequired(false)
                .HasColumnType("DateTime");

            builder.ToTable("Clients");
        }
    }
}