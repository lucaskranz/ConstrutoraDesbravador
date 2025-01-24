using AutoMapper;
using ConstrutoraDesbravador.API.DTOs;
using ConstrutoraDesbravador.Business.Interfaces;
using ConstrutoraDesbravador.Business.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace ConstrutoraDesbravador.API.Controllers
{
    [Route("api/projetos")]
    public class ProjetoController : BaseController
    {
        private readonly IProjetoService _projetoService;
        public readonly IMapper _mapper;

        public ProjetoController(INotificador notificador, IProjetoService projetoService, IMapper mapper) : base(notificador)
        {
            _projetoService = projetoService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<PaginacaoResult<ProjetoDTO>> ObterTodos(int page = 1, int size = 10)
        {
            var projetos = await _projetoService.Obter();

            var total = projetos.Count();
            var skip = (page - 1) * size;
            var take = size;

            projetos = projetos
                .Skip(skip)
                .Take(take);

            return new PaginacaoResult<ProjetoDTO>
            {
                Total = total,
                Size = size,
                Page = page,
                Items = _mapper.Map<IEnumerable<ProjetoDTO>>(projetos.ToList())
            };
        }

        [HttpGet("{id:int}")]
        public async Task<ProjetoDTO> ObterPorId(int id)
        {
            return _mapper.Map<ProjetoDTO>(await _projetoService.ObterPorId(id));
        }

        [HttpPost]
        public async Task<ActionResult<ProjetoSemResponsavelDTO>> Adicionar([FromBody]ProjetoSemResponsavelDTO projetoDTO)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            var projeto = _mapper.Map<Projeto>(projetoDTO);
            await _projetoService.Adicionar(projeto);

            return CustomResponse(HttpStatusCode.Created, projeto);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Atualizar(int id, [FromBody]ProjetoSemResponsavelDTO projetoDTO)
        {            
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            var projetoAtualizacao = _mapper.Map<Projeto>(projetoDTO);
            await _projetoService.Atualizar(projetoAtualizacao);

            return CustomResponse(HttpStatusCode.OK, projetoAtualizacao);
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<ProjetoSemResponsavelDTO>> Excluir(int id)
        {
            var projeto = await _projetoService.ObterPorId(id);
            if (projeto == null) return NotFound();

            await _projetoService.Remover(projeto);

            return CustomResponse(HttpStatusCode.NoContent);
        }

        [HttpPost("{id}/vincular-funcionarios")]
        public async Task<ActionResult> VincularFuncionarios(int id, string idsFuncionarios)
        {
            if (string.IsNullOrEmpty(idsFuncionarios))
            {
                return BadRequest("É obrigatório selecionar funcionários para vincular.");
            }

            await _projetoService.VincularFuncionarios(id, idsFuncionarios);

            return CustomResponse(HttpStatusCode.Created);
        }

    }
}
