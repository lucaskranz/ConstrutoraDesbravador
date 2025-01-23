using AutoMapper;
using ConstrutoraDesbravador.API.DTOs;
using ConstrutoraDesbravador.Business.Interfaces;
using ConstrutoraDesbravador.Business.Models;
using ConstrutoraDesbravador.Business.Services;
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
        public async Task<IEnumerable<ProjetoDTO>> ObterTodos()
        {
            return _mapper.Map<IEnumerable<ProjetoDTO>>(await _projetoService.Obter());
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

            await _projetoService.Adicionar(_mapper.Map<Projeto>(projetoDTO));

            return CustomResponse(HttpStatusCode.Created, projetoDTO);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Atualizar(int id, [FromBody]ProjetoSemResponsavelDTO projetoDTO)
        {            
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            projetoDTO.Id = id;
            var produtoAtualizacao = _mapper.Map<Projeto>(projetoDTO);      
            await _projetoService.Atualizar(produtoAtualizacao);

            return CustomResponse(HttpStatusCode.NoContent);
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<ProjetoSemResponsavelDTO>> Excluir(int id)
        {
            var produto = await _projetoService.ObterPorId(id);
            if (produto == null) return NotFound();

            await _projetoService.Remover(produto);

            return CustomResponse(HttpStatusCode.NoContent);
        }

        [HttpPost("{id}/vincular-funcionarios")]
        public async Task<ActionResult> VincularFuncionarios(int id, string idsFuncionarios)
        {
            await _projetoService.VincularFuncionarios(id, idsFuncionarios);

            return CustomResponse(HttpStatusCode.Created);
        }

    }
}
