using Core.Business.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Core.Data.Mappings
{
    public class CategoriaMapping : IEntityTypeConfiguration<Categoria>
    {
        public void Configure(EntityTypeBuilder<Categoria> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.Nome)
                .IsRequired()
                .HasColumnType("varchar(100)");

            // 1 : N => Categoria : SubCategorias
            builder.HasMany(c => c.SubCategorias)
                .WithOne(s => s.SubCategoria)
                .HasForeignKey(s => s.CategoriaId)
                .IsRequired(false);

            builder.ToTable("Categorias");
        }
    }
}