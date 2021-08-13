using Core.Business.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Core.Data.Mappings
{
    public class SupplierMapping : IEntityTypeConfiguration<Supplier>
    {
        public void Configure(EntityTypeBuilder<Supplier> builder)
        {
            builder.HasKey(f => f.Id);
 
            builder.Property(f => f.Name)
                .IsRequired()
                .HasColumnType("varchar(200)");

            builder.Property(f => f.Document)
                .IsRequired()
                .HasColumnType("char(14)");

            builder.Property(m => m.TypeSupplier)
                .HasColumnType("varchar(30)")
                .HasConversion<string>();

            builder.Property(f => f.CreatedBy)
                .IsRequired(false)
                .HasColumnType("varchar(max)");
            builder.Property(f => f.CreatedDate)
                .IsRequired(false)
                .HasColumnType("DateTime")
                .HasDefaultValueSql("GETDATE()")
                .ValueGeneratedOnAdd();

            builder.Property(f => f.UpdatedBy)
                .IsRequired(false)
                .HasColumnType("varchar(max)");
            builder.Property(f => f.UpdatedDate)
                .IsRequired(false)
                .HasColumnType("DateTime");

            // 1 : N => Supplier : Products
            builder.HasMany(f => f.Products)
                .WithOne(p => p.Supplier)
                .HasForeignKey(p => p.SupplierId);

            builder.ToTable("Suppliers");
        }
    }
}