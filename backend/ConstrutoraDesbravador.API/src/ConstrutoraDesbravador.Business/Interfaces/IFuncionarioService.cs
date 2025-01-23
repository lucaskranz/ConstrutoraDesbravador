using ConstrutoraDesbravador.Business.Models;

namespace ConstrutoraDesbravador.Business.Interfaces
{
    public interface IFuncionarioService
    {
        Task<IEnumerable<Funcionario>> Obter();
        Task Adicionar(Funcionario funcionario);
        Task AdicionarVarios(IList<Funcionario> funcionarios);
        Task Atualizar(Funcionario funcionario);
        Task Remover(int id);
    }
}
