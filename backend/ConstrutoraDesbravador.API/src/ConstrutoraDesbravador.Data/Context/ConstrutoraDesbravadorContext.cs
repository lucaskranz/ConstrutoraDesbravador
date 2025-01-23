using ConstrutoraDesbravador.Business.Models;
using Microsoft.EntityFrameworkCore;

namespace ConstrutoraDesbravador.Data.Context
{
    public class ConstrutoraDesbravadorContext : DbContext
    {
        public ConstrutoraDesbravadorContext(DbContextOptions<ConstrutoraDesbravadorContext> options) : base(options) { }

        public DbSet<Projeto> Projetos { get; set; }
        public DbSet<Funcionario> Funcionarios { get; set; }
        public DbSet<ProjetoFuncionario> ProjetoFuncionarios { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            foreach (var property in modelBuilder.Model.GetEntityTypes()
                .SelectMany(e => e.GetProperties()
                    .Where(p => p.ClrType == typeof(string))))
                property.SetColumnType("varchar(100)");

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ConstrutoraDesbravadorContext).Assembly);

            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys())) relationship.DeleteBehavior = DeleteBehavior.ClientSetNull;

            base.OnModelCreating(modelBuilder);
        }
    }
}
