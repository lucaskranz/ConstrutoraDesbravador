using Moq;
using ConstrutoraDesbravador.Business.Interfaces;
using ConstrutoraDesbravador.Business.Models;
using ConstrutoraDesbravador.Business.Services;
using ConstrutoraDesbravador.Business.Enums;

public class ProjetoServiceTests
{
    private readonly Mock<IProjetoRepository> _projetoRepositoryMock;
    private readonly Mock<IFuncionarioRepository> _funcionarioRepositoryMock;
    private readonly Mock<INotificador> _notificadorMock;
    private readonly ProjetoService _service;

    public ProjetoServiceTests()
    {
        _projetoRepositoryMock = new Mock<IProjetoRepository>();
        _funcionarioRepositoryMock = new Mock<IFuncionarioRepository>();
        _notificadorMock = new Mock<INotificador>();

        _service = new ProjetoService(
            _projetoRepositoryMock.Object,
            _notificadorMock.Object,
            _funcionarioRepositoryMock.Object
        );
    }

    [Fact]
    public async Task Obter_DeveRetornarProjetosExistentes()
    {
        // Arrange
        var projetos = new List<Projeto>
        {
            new Projeto { Id = 1, Nome = "Projeto A", StatusProjeto = StatusProjetoEnum.EmAnalise }
        };

        _projetoRepositoryMock
            .Setup(repo => repo.ObterProjetosResponsaveisFuncionarios())
            .ReturnsAsync(projetos);

        // Act
        var result = await _service.Obter();

        // Assert
        Assert.NotNull(result);
        Assert.Equal(projetos.Count, result.Count());
    }       
       
    [Fact]
    public async Task Remover_DeveRemoverProjetoQuandoValido()
    {
        // Arrange
        var projeto = new Projeto { Id = 1, Nome = "Projeto A", StatusProjeto = StatusProjetoEnum.EmAnalise };

        _projetoRepositoryMock
            .Setup(repo => repo.RemoverProjetoFuncionario(It.IsAny<int>()))
            .Returns(Task.CompletedTask);

        // Act
        await _service.Remover(projeto);

        // Assert
        _projetoRepositoryMock.Verify(repo => repo.RemoverProjetoFuncionario(It.IsAny<int>()), Times.Once);
    }

    [Fact]
    public async Task VincularFuncionarios_DeveVincularFuncionariosCorretamente()
    {
        // Arrange
        var projetoId = 1;
        var funcionariosIds = "1,2,3";
        var funcionarios = new List<Funcionario>
        {
            new Funcionario { Id = 1, Nome = "Funcionario 1" },
            new Funcionario { Id = 2, Nome = "Funcionario 2" }
        };

        _funcionarioRepositoryMock
            .Setup(repo => repo.Buscar(It.IsAny<System.Linq.Expressions.Expression<System.Func<Funcionario, bool>>>()))
            .ReturnsAsync(funcionarios);

        _projetoRepositoryMock
            .Setup(repo => repo.VincularFuncionarios(It.IsAny<int>(), It.IsAny<List<Funcionario>>()))
            .Returns(Task.CompletedTask);

        // Act
        await _service.VincularFuncionarios(projetoId, funcionariosIds);

        // Assert
        _projetoRepositoryMock.Verify(repo => repo.VincularFuncionarios(It.IsAny<int>(), It.IsAny<List<Funcionario>>()), Times.Once);
    }

    [Fact]
    public void PodeExcluir_DeveRetornarTrueParaStatusPermitidos()
    {
        // Arrange
        var status = StatusProjetoEnum.EmAnalise;

        // Act
        var result = _service.PodeExcluir(status);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void PodeExcluir_DeveRetornarFalseParaStatusProibidos()
    {
        // Arrange
        var status = StatusProjetoEnum.Iniciado;

        // Act
        var result = _service.PodeExcluir(status);

        // Assert
        Assert.False(result);
    }

    [Fact]
    public void Dispose_DeveDestruirRepositorioQuandoServiceForDestruido()
    {
        // Arrange
        _projetoRepositoryMock.Setup(repo => repo.Dispose()).Verifiable();

        // Act
        _service.Dispose();

        // Assert
        _projetoRepositoryMock.Verify(repo => repo.Dispose(), Times.Once);
    }
}
