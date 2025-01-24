using ConstrutoraDesbravador.Business.Interfaces;
using ConstrutoraDesbravador.Business.Models;
using ConstrutoraDesbravador.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace ConstrutoraDesbravador.Data.Repository
{
    public class FuncionarioRepository : Repository<Funcionario>, IFuncionarioRepository
    {
        public FuncionarioRepository(ConstrutoraDesbravadorContext context) : base(context) { }

        public bool ExisteEmail(string email)
        {
            var funcionarios = Db.Funcionarios
                .AsNoTracking()
                .Where(x => x.Email.ToLower() == email.ToLower());
                
            return funcionarios.Any();
        }

        public async Task<IEnumerable<Funcionario>> ObterFuncionariosProjetos()
        {
            var funcionarios = Db.Funcionarios
                .AsNoTracking()
                .Include(x => x.ProjetosResponsavel)
                .Include(x => x.ProjetosVinculados);

            return await funcionarios.ToListAsync();
        }

        public async Task<Funcionario> ObterFuncionarioProjeto(int id)
        {
            return Db.Funcionarios
                .AsNoTracking()
                .Include(x => x.ProjetosResponsavel)
                .Include(x => x.ProjetosVinculados)
                .FirstOrDefault(x => x.Id == id);
        }

        public async Task RemoverProjetoFuncionario(int id)
        {            
            var projetoFuncionarios = Db.ProjetoFuncionarios.Where(x => x.FuncionarioId == id).ToList();
            if (projetoFuncionarios.Any())
            {
                Db.ProjetoFuncionarios.RemoveRange(projetoFuncionarios);
                Db.SaveChanges();            
            }

            await Remover(id);
        }
    }
}
