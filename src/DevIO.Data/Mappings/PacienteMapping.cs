using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using MRP.Business.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MRP.Data.Mappings
{
    public class PacienteMapping : IEntityTypeConfiguration<Paciente>
    {
        public void Configure(EntityTypeBuilder<Paciente> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(p => p.Nome)
                .IsRequired()
                .HasColumnType("varchar(100)");

            builder.Property(p => p.Apelido)
                .IsRequired()
                .HasColumnType("varchar(100)");

            builder.Property(p => p.NumeroUtente)
                .IsRequired()
                .HasColumnType("varchar(30)");

            builder.Property(p => p.NumeroIdentificacao)
                .IsRequired()
                .HasColumnType("varchar(30)");

            builder.Property(p => p.Email)
                .IsRequired()
                .HasColumnType("varchar(200)");

            builder.Property(p => p.Telemovel)
                .IsRequired()
                .HasColumnType("varchar(30)");

            // 1 : 1 => Paciente : Endereco
            builder.HasOne(p => p.Endereco)
                .WithOne(e => e.Paciente);

            builder.ToTable("Pacientes");
        }
    }
}
