using ConstrutoraDesbravador.Business.Models;

namespace ConstrutoraDesbravador.Business.Interfaces
{
    public interface IProjetoService
    {
        Task<IEnumerable<Projeto>> Obter();
        Task<Projeto> ObterPorId(int id);
        Task Adicionar(Projeto projeto);
        Task Atualizar(Projeto projeto);
        Task Remover(Projeto projeto);
        Task VincularFuncionarios(int idProjeto, string idsFuncionarios);  
    }
}
