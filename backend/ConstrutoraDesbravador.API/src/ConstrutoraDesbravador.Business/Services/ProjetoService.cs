using ConstrutoraDesbravador.Business.Interfaces;
using ConstrutoraDesbravador.Business.Models;
using ConstrutoraDesbravador.Business.Validations;

namespace ConstrutoraDesbravador.Business.Services
{
    public class ProjetoService : BaseService, IProjetoService
    {
        private readonly IProjetoRepository _projetoRepository;

        public ProjetoService(IProjetoRepository projetoRepository,
                              INotificador notificador) : base(notificador)
        {
            _projetoRepository = projetoRepository;
        }

        public async Task Adicionar(Projeto projeto)
        {
            if (!ExecutarValidacao(new ProjetoValidation(), projeto)) return;

            var projetoExistente = _projetoRepository.ObterPorId(projeto.Id);

            if (projetoExistente != null)
            {
                Notificar("Já existe um projeto com o ID informado!");
                return;
            }

            await _projetoRepository.Adicionar(projeto);
        }

        public async Task Atualizar(Projeto projeto)
        {
            if (!ExecutarValidacao(new ProjetoValidation(), projeto)) return;

            await _projetoRepository.Atualizar(projeto);
        }

        public async Task Remover(int id)
        {
            await _projetoRepository.Remover(id);
        }

        public void Dispose()
        {
            _projetoRepository?.Dispose();
        }
    }
}
