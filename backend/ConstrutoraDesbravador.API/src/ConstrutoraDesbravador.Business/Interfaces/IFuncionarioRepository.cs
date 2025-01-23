using ConstrutoraDesbravador.Business.Models;

namespace ConstrutoraDesbravador.Business.Interfaces
{
    public interface IFuncionarioRepository : IRepository<Funcionario>
    {
        bool ExisteEmail(string email);
    }
}
