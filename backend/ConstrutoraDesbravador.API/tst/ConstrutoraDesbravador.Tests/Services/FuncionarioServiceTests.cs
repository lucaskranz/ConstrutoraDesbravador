using Moq;
using AutoMapper;
using ConstrutoraDesbravador.Business.Models;
using ConstrutoraDesbravador.Business.Interfaces;
using ConstrutoraDesbravador.Business.Services;

public class FuncionarioServiceTests
{
    private readonly Mock<IFuncionarioRepository> _funcionarioRepositoryMock;
    private readonly Mock<RandomUserService> _randomUserServiceMock;
    private readonly Mock<INotificador> _notificadorMock;
    private readonly Mock<IMapper> _mapperMock;
    private readonly FuncionarioService _service;

    public FuncionarioServiceTests()
    {
        _funcionarioRepositoryMock = new Mock<IFuncionarioRepository>();
        _notificadorMock = new Mock<INotificador>();
        _mapperMock = new Mock<IMapper>();

        _service = new FuncionarioService(
            _funcionarioRepositoryMock.Object,
            _notificadorMock.Object,
            null,
            _mapperMock.Object
        );
    }

    [Fact]
    public async Task Obter_DeveRetornarFuncionariosExistentesOuAdicionarAleatorios()
    {
        // Arrange
        var funcionarios = new List<Funcionario>
        {
            new Funcionario { Id = 1, Nome = "Funcionario A" }
        };

        _funcionarioRepositoryMock
            .Setup(repo => repo.ObterFuncionariosProjetos())
            .ReturnsAsync(funcionarios);

        // Act
        var result = await _service.Obter();

        // Assert
        Assert.NotNull(result);
        Assert.Equal(funcionarios.Count, result.Count());
    }
        
    [Fact]
    public async Task AdicionarVarios_DeveValidarFuncionariosAntesDeAdicionar()
    {
        // Arrange
        var funcionarios = new List<Funcionario>
        {
            new Funcionario { Id = 1, Nome = "Funcionario A" }
        };

        _funcionarioRepositoryMock
            .Setup(repo => repo.AdicionarVarios(It.IsAny<List<Funcionario>>()))
            .Returns(Task.CompletedTask);

        // Act
        await _service.AdicionarVarios(funcionarios);

        // Assert
        _funcionarioRepositoryMock.Verify(repo => repo.AdicionarVarios(It.IsAny<List<Funcionario>>()), Times.Once);
    }

    [Fact]
    public void Dispose_DeveDestruirRepositorioQuandoServiceForDestruido()
    {
        // Arrange
        _funcionarioRepositoryMock.Setup(repo => repo.Dispose()).Verifiable();

        // Act
        _service.Dispose();

        // Assert
        _funcionarioRepositoryMock.Verify(repo => repo.Dispose(), Times.Once);
    }
}
