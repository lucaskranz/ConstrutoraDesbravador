using ConstrutoraDesbravador.Business.Models;

namespace ConstrutoraDesbravador.Business.Interfaces
{
    public interface IFuncionarioService
    {
        Task<IEnumerable<Funcionario>> Obter();
        Task<Funcionario> ObterPorId(int id);
        Task<IEnumerable<Funcionario>> AdicionarAleatorios(int quantidade);
        Task AdicionarVarios(IList<Funcionario> funcionarios);
        Task Remover(int id);
    }
}
