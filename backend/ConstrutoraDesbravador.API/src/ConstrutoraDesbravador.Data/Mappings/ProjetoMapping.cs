using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using ConstrutoraDesbravador.Business.Models;
using ConstrutoraDesbravador.Business.Enums;
using System.Reflection.Emit;

namespace ConstrutoraDesbravador.Data.Mappings
{
    public class ProjetoMapping : IEntityTypeConfiguration<Projeto>
    {
        public void Configure(EntityTypeBuilder<Projeto> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(c => c.Nome)
                .IsRequired()
                .HasColumnType("varchar(50)");

            builder.Property(c => c.Descricao)
                .IsRequired()
                .HasColumnType("varchar(250)");

            builder.Property(c => c.DataInicio)
                .IsRequired()
                .HasColumnType("date");

            builder.Property(c => c.DataTermino)
                .IsRequired()
                .HasColumnType("date");

            builder.HasOne(p => p.Responsavel)
                .WithMany(r => r.ProjetosResponsavel)
                .HasForeignKey(p => p.ResponsavelId);

            builder.HasMany(f => f.Funcionarios)
               .WithMany(p => p.ProjetosVinculados)
               .UsingEntity<ProjetoFuncionario>();

            builder.ToTable("Projeto");
        }
    }

}
