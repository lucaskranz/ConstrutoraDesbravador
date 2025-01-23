using ConstrutoraDesbravador.Business.Enums;
using ConstrutoraDesbravador.Business.Interfaces;
using ConstrutoraDesbravador.Business.Models;
using ConstrutoraDesbravador.Business.Validations;

namespace ConstrutoraDesbravador.Business.Services
{
    public class ProjetoService : BaseService, IProjetoService
    {
        private readonly IProjetoRepository _projetoRepository;
        private readonly IFuncionarioRepository _funcionarioRepository;

        public ProjetoService(IProjetoRepository projetoRepository,
                              INotificador notificador,
                              IFuncionarioRepository funcionarioRepository) : base(notificador)
        {
            _projetoRepository = projetoRepository;
            _funcionarioRepository = funcionarioRepository;
        }

        public async Task<IEnumerable<Projeto>> Obter()
        {
            return await _projetoRepository.ObterProjetosResponsaveisFuncionarios();
        }

        public async Task Adicionar(Projeto projeto)
        {
            if (!ExecutarValidacao(new ProjetoValidation(_projetoRepository), projeto)) return;

            await _projetoRepository.Adicionar(projeto);
        }

        public async Task Atualizar(Projeto projeto)
        {
            if (!ExecutarValidacao(new ProjetoValidation(_projetoRepository), projeto)) return;

            await _projetoRepository.Atualizar(projeto);
        }

        public async Task Remover(Projeto projeto)
        {
            if (!PodeExcluir(projeto.StatusProjeto))
            {
                Notificar("O status atual do projeto não permite a exclusão.");
                return;
            }

            await _projetoRepository.Remover(projeto.Id);
        }

        public void Dispose()
        {
            _projetoRepository?.Dispose();
        }

        public async Task<Projeto> ObterPorId(int id)
        {
            return await _projetoRepository.ObterProjetoResponsavelFuncionarios(id);
        }

        private bool PodeExcluir(StatusProjetoEnum status)
        {
            var statusProibidos = new[]
            {
                StatusProjetoEnum.Iniciado,
                StatusProjetoEnum.EmAndamento,
                StatusProjetoEnum.Encerrado
            };

            return !statusProibidos.Contains(status);
        }

        public async Task VincularFuncionarios(int idProjeto, string idsFuncionarios)
        {
            var funcionariosIds = idsFuncionarios.Split(',').Select(id => int.TryParse(id, out var numero) ? numero : (int?)null);
            var funcionarios = await _funcionarioRepository.Buscar(x => funcionariosIds.Contains(x.Id) 
                                                                     && !x.ProjetosVinculados.Any(c => c.Id == idProjeto));

            await _projetoRepository.VincularFuncionarios(idProjeto, funcionarios.ToList());
        }
    }
}
