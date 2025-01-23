using ConstrutoraDesbravador.Business.Interfaces;
using ConstrutoraDesbravador.Business.Models;
using Microsoft.AspNetCore.Mvc;

namespace ConstrutoraDesbravador.API.Controllers
{

    [Route("api/funcionarios")]
    public class FuncionarioController : BaseController
    {
        public readonly IFuncionarioService _funcionarioService;

        public FuncionarioController(INotificador notificador, IFuncionarioService funcionarioService) : base(notificador)
        {
            _funcionarioService = funcionarioService;
        }

        [HttpGet]
        public async Task<IEnumerable<Funcionario>> ObterTodos()
        {
            return await _funcionarioService.Obter();
        }

    }
}
