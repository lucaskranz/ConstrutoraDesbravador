using AutoMapper;
using ConstrutoraDesbravador.API.Controllers;
using ConstrutoraDesbravador.API.DTOs;
using ConstrutoraDesbravador.Business.Interfaces;
using ConstrutoraDesbravador.Business.Models;
using Microsoft.AspNetCore.Mvc;
using Moq;

public class FuncionarioControllerTests
{
    private readonly Mock<IFuncionarioService> _funcionarioServiceMock;
    private readonly Mock<IMapper> _mapperMock;
    private readonly Mock<INotificador> _notificadorMock;
    private readonly FuncionarioController _controller;

    public FuncionarioControllerTests()
    {
        _funcionarioServiceMock = new Mock<IFuncionarioService>();
        _mapperMock = new Mock<IMapper>();
        _notificadorMock = new Mock<INotificador>();

        _controller = new FuncionarioController(
            _notificadorMock.Object,
            _funcionarioServiceMock.Object,
            _mapperMock.Object
        );
    }

    [Fact]
    public async Task ObterTodos_DeveRetornarPaginacaoResult()
    {
        // Arrange
        var funcionarios = new List<Funcionario>
        {
            new Funcionario { Id = 1, Nome = "John" },
            new Funcionario { Id = 2, Nome = "Doe" }
        };

        var funcionariosDTO = funcionarios.Select(f => new FuncionarioProjetosDTO { Id = f.Id, Nome = f.Nome });

        _funcionarioServiceMock
            .Setup(service => service.Obter())
            .ReturnsAsync(funcionarios);

        _mapperMock
            .Setup(mapper => mapper.Map<IEnumerable<FuncionarioProjetosDTO>>(It.IsAny<List<Funcionario>>()))
            .Returns(funcionariosDTO);

        // Act
        var result = await _controller.ObterTodos(page: 1, size: 10);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(funcionarios.Count, result.Total);
        Assert.Equal(10, result.Size);
        Assert.Equal(1, result.Page);
        Assert.Equal(funcionarios.Count, result.Items.Count());
    }

    [Fact]
    public async Task ObterPorId_DeveRetornarFuncionarioDTO()
    {
        // Arrange
        var funcionario = new Funcionario { Id = 1, Nome = "John" };
        var funcionarioDTO = new FuncionarioProjetosDTO { Id = 1, Nome = "John" };

        _funcionarioServiceMock
            .Setup(service => service.ObterPorId(funcionario.Id))
            .ReturnsAsync(funcionario);

        _mapperMock
            .Setup(mapper => mapper.Map<FuncionarioProjetosDTO>(funcionario))
            .Returns(funcionarioDTO);

        // Act
        var result = await _controller.ObterPorId(1);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(funcionarioDTO.Id, result.Id);
        Assert.Equal(funcionarioDTO.Nome, result.Nome);
    }
}