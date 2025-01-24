using ConstrutoraDesbravador.Business.Interfaces;
using ConstrutoraDesbravador.Business.Models;
using ConstrutoraDesbravador.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace ConstrutoraDesbravador.Data.Repository
{
    public class ProjetoRepository : Repository<Projeto>, IProjetoRepository
    {
        public ProjetoRepository(ConstrutoraDesbravadorContext context) : base(context)
        {
        }

        public bool ExisteNomeProjeto(string nome)
        {
            return Db.Projetos.AsNoTracking()
                .Any(x => x.Nome.ToLower() == nome.ToLower());
        }

        public async Task<Projeto> ObterProjetoResponsavelFuncionarios(int id)
        {
            return await Db.Projetos
                .AsNoTracking()
                .Include(x => x.Responsavel)
                .Include(x => x.Funcionarios)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<IEnumerable<Projeto>> ObterProjetosResponsaveisFuncionarios()
        {
            var projetos = Db.Projetos
                .AsNoTracking()
                .Include(x => x.Responsavel)
                .Include(x => x.Funcionarios);

            return await projetos.ToListAsync();
        }

        public async Task RemoverProjetoFuncionario(int id)
        {
            var projetoFuncionarios = Db.ProjetoFuncionarios.Where(x => x.ProjetoId == id).ToList();
            if(projetoFuncionarios.Any())
            {
                Db.ProjetoFuncionarios.RemoveRange(projetoFuncionarios);
                Db.SaveChanges();
            }

            await Remover(id);
        }

        public async Task VincularFuncionarios(int idProjeto, List<Funcionario> funcionarios)
        {
            var projetoFuncionarios = new List<ProjetoFuncionario>();
            foreach (var funcionario in funcionarios)
            {
                var projetoFuncionario = new ProjetoFuncionario
                {
                    FuncionarioId = funcionario.Id,
                    ProjetoId = idProjeto
                };

                projetoFuncionarios.Add(projetoFuncionario);
            }

            await Db.ProjetoFuncionarios.AddRangeAsync(projetoFuncionarios);
            Db.SaveChanges();
        }
    }
}
