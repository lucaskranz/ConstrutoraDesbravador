﻿using AutoMapper;
using ConstrutoraDesbravador.Business.Interfaces;
using ConstrutoraDesbravador.Business.Models;
using ConstrutoraDesbravador.Business.Validations;

namespace ConstrutoraDesbravador.Business.Services
{
    public class FuncionarioService : BaseService, IFuncionarioService
    {
        private readonly IFuncionarioRepository _funcionarioRepository;
        private readonly RandomUserService _randomUserService;
        public readonly IMapper _mapper;

        public FuncionarioService(IFuncionarioRepository funcionarioRepository,
                              INotificador notificador,
                              RandomUserService randomUserservice,
                              IMapper mapper) : base(notificador)
        {
            _funcionarioRepository = funcionarioRepository;
            _randomUserService = randomUserservice;
            _mapper = mapper;
        }
        public async Task<IEnumerable<Funcionario>> Obter()
        {
            var funcionarios = await _funcionarioRepository.ObterTodos();

            if (!funcionarios.Any())
            {
                var randomUsers = await _randomUserService.GetRandomUsersAsync();
                funcionarios = _mapper.Map<List<Funcionario>>(randomUsers);
                await AdicionarVarios(funcionarios);
            }

            return funcionarios;
        }

        public async Task Adicionar(Funcionario funcionario)
        {
            if (!ExecutarValidacao(new FuncionarioValidation(), funcionario)) return;

            await _funcionarioRepository.Adicionar(funcionario);
        }
        public async Task AdicionarVarios(IList<Funcionario> funcionarios)
        {
            var funcionariosValidados = funcionarios.Where(x => ExecutarValidacao(new FuncionarioValidation(), x)).ToList();            
            await _funcionarioRepository.AdicionarVarios(funcionariosValidados);
        }

        public async Task Atualizar(Funcionario funcionario)
        {
            if (!ExecutarValidacao(new FuncionarioValidation(), funcionario)) return;

            await _funcionarioRepository.Atualizar(funcionario);
        }

        public async Task Remover(int id)
        {
            var funcionario = await _funcionarioRepository.ObterPorId(id);

            if (funcionario == null)
            {
                Notificar("Funcionario não existe!");
                return;
            }

            await _funcionarioRepository.Remover(id);
        }

        public void Dispose()
        {
            _funcionarioRepository?.Dispose();
        }

    }
}
