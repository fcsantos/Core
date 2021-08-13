using Core.Business.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Core.Data.Mappings
{
    public class CategoryMapping : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.HasKey(c => c.Id); 

            builder.Property(c => c.Name)
                .IsRequired()
                .HasColumnType("varchar(100)");

            builder.Property(c => c.CreatedBy)
                .IsRequired()
                .HasColumnType("varchar(max)");
            builder.Property(c => c.CreatedDate)
                .IsRequired()
                .HasColumnType("DateTime")
                .HasDefaultValueSql("GETDATE()");

            builder.Property(c => c.UpdatedBy)
                .IsRequired(false)
                .HasColumnType("varchar(max)");
            builder.Property(c => c.UpdatedDate)
                .IsRequired(false)
                .HasColumnType("DateTime");

            // 1 : N => Category : ParentCategories
            builder.HasMany(c => c.ParentCategories)
                .WithOne(s => s.ParentCategory)
                .HasForeignKey(s => s.CategoryId)
                .IsRequired(false);

            builder.ToTable("Categories");
        }
    }
}