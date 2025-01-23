using AutoMapper;
using ConstrutoraDesbravador.API.DTOs;
using ConstrutoraDesbravador.Business.Interfaces;
using ConstrutoraDesbravador.Business.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Net;

namespace ConstrutoraDesbravador.API.Controllers
{

    [Route("api/funcionarios")]
    public class FuncionarioController : BaseController
    {
        public readonly IFuncionarioService _funcionarioService;
        public readonly IMapper _mapper;

        public FuncionarioController(INotificador notificador, IFuncionarioService funcionarioService, IMapper mapper) : base(notificador)
        {
            _funcionarioService = funcionarioService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IEnumerable<FuncionarioDTO>> ObterTodos()
        {
            return _mapper.Map<IEnumerable<FuncionarioDTO>>(await _funcionarioService.Obter());
        }

        [HttpPost("adicionar-aleatorios")]
        public async Task<ActionResult<FuncionarioDTO>> AdicionarAleatorios()
        {
            var funcionariosAleatorios = _mapper.Map<IEnumerable<FuncionarioDTO>>(await _funcionarioService.AdicionarAleatorios());

            return CustomResponse(HttpStatusCode.Created, funcionariosAleatorios);
        }

        [HttpDelete]
        public async Task<ActionResult> Excluir(int id)
        {
            await _funcionarioService.Remover(id);

            return CustomResponse(HttpStatusCode.NoContent);
        }
    }
}
