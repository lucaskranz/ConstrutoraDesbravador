using ConstrutoraDesbravador.Business.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace ConstrutoraDesbravador.Data.Mappings
{
    public class FuncionarioMapping : IEntityTypeConfiguration<Funcionario>
    {
        public void Configure(EntityTypeBuilder<Funcionario> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(c => c.Nome)
                .IsRequired()
                .HasColumnType("varchar(30)");

            builder.Property(c => c.Sobrenome)
                .IsRequired()
                .HasColumnType("varchar(30)");

            builder.Property(c => c.Email)
                .IsRequired()
                .HasColumnType("varchar(50)");

            builder.ToTable("Funcionario");
        }
    }
}
