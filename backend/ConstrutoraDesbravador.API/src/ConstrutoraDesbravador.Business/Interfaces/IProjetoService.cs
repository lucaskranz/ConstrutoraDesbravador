using ConstrutoraDesbravador.Business.Models;

namespace ConstrutoraDesbravador.Business.Interfaces
{
    public interface IProjetoService
    {
        Task Adicionar(Projeto projeto);
        Task Atualizar(Projeto projeto);
        Task Remover(int id);
    }
}
