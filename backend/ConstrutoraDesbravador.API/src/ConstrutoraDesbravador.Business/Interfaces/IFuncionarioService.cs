using ConstrutoraDesbravador.Business.Models;

namespace ConstrutoraDesbravador.Business.Interfaces
{
    public interface IFuncionarioService
    {
        Task<IEnumerable<Funcionario>> Obter();
        Task<IEnumerable<Funcionario>> AdicionarAleatorios();
        Task AdicionarVarios(IList<Funcionario> funcionarios);
        Task Remover(int id);
    }
}
