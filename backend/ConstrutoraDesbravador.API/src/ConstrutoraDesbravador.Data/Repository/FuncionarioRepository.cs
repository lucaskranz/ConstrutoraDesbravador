using ConstrutoraDesbravador.Business.Interfaces;
using ConstrutoraDesbravador.Business.Models;
using ConstrutoraDesbravador.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace ConstrutoraDesbravador.Data.Repository
{
    public class FuncionarioRepository : Repository<Funcionario>, IFuncionarioRepository
    {
        public FuncionarioRepository(ConstrutoraDesbravadorContext context) : base(context)
        {
        }

        public bool ExisteEmail(string email)
        {
            var funcionarios = Db.Funcionarios
                .AsNoTracking()
                .Where(x => x.Email.ToLower() == email.ToLower());
                
            return funcionarios.Any();
        }
    }
}
