using ConstrutoraDesbravador.Business.Models;

namespace ConstrutoraDesbravador.Business.Interfaces
{
    public interface IFuncionarioRepository : IRepository<Funcionario>
    {
        Task<IEnumerable<Funcionario>> ObterFuncionariosProjetos();
        Task<Funcionario> ObterFuncionarioProjeto(int id);
        bool ExisteEmail(string email);
    }
}
