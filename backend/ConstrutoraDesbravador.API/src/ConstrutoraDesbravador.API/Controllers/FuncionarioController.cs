﻿using AutoMapper;
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
        public async Task<PaginacaoResult<FuncionarioProjetosDTO>> ObterTodos(int page = 1, int size = 10)
        {
            var funcionarios = await _funcionarioService.Obter();

            var total = funcionarios.Count();
            var skip = (page - 1) * size;
            var take = size;

            funcionarios = funcionarios
                .Skip(skip)
                .Take(take);

            return new PaginacaoResult<FuncionarioProjetosDTO>
            {
                Total = total,
                Size = size,
                Page = page,
                Items = _mapper.Map<IEnumerable<FuncionarioProjetosDTO>>(funcionarios.ToList())
            };
        }

        [HttpGet("{id}")]
        public async Task<FuncionarioProjetosDTO> ObterPorId(int id)
        {
            return _mapper.Map<FuncionarioProjetosDTO>(await _funcionarioService.ObterPorId(id));
        }

        [HttpPost("adicionar-aleatorios")]
        public async Task<ActionResult<FuncionarioDTO>> AdicionarAleatorios(int quantidade = 5)
        {
            var funcionariosAleatorios = _mapper.Map<IEnumerable<FuncionarioProjetosDTO>>(await _funcionarioService.AdicionarAleatorios(quantidade));

            return CustomResponse(HttpStatusCode.Created, funcionariosAleatorios);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Excluir(int id)
        {
            await _funcionarioService.Remover(id);

            return CustomResponse(HttpStatusCode.NoContent);
        }
    }
}
