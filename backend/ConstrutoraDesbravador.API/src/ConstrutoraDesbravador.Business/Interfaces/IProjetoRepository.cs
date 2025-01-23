using ConstrutoraDesbravador.Business.Models;

namespace ConstrutoraDesbravador.Business.Interfaces
{
    public interface IProjetoRepository: IRepository<Projeto>
    {
        Task<IEnumerable<Projeto>> ObterProjetosResponsaveisFuncionarios();
        Task<Projeto> ObterProjetoResponsavelFuncionarios(int id);
        bool ExisteNomeProjeto(string nome);
        Task VincularFuncionarios(int idProjeto, List<Funcionario> funcionarios);
    }
}
