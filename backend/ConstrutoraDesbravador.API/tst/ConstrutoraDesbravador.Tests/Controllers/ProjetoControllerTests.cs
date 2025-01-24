using Moq;
using AutoMapper;
using ConstrutoraDesbravador.API.Controllers;
using ConstrutoraDesbravador.API.DTOs;
using ConstrutoraDesbravador.Business.Interfaces;
using ConstrutoraDesbravador.Business.Models;
using Microsoft.AspNetCore.Mvc;

public class ProjetoControllerTests
{
    private readonly Mock<IProjetoService> _projetoServiceMock;
    private readonly Mock<IMapper> _mapperMock;
    private readonly Mock<INotificador> _notificadorMock;
    private readonly ProjetoController _controller;

    public ProjetoControllerTests()
    {
        _projetoServiceMock = new Mock<IProjetoService>();
        _mapperMock = new Mock<IMapper>();
        _notificadorMock = new Mock<INotificador>();

        _controller = new ProjetoController(
            _notificadorMock.Object,
            _projetoServiceMock.Object,
            _mapperMock.Object
        );
    }

    [Fact]
    public async Task ObterTodos_DeveRetornarPaginacaoResult()
    {
        // Arrange
        var projetos = new List<Projeto>
        {
            new Projeto { Id = 1, Nome = "Projeto A" },
            new Projeto { Id = 2, Nome = "Projeto B" }
        };

        var projetosDTO = projetos.Select(p => new ProjetoDTO { Id = p.Id, Nome = p.Nome });

        _projetoServiceMock
            .Setup(service => service.Obter())
            .ReturnsAsync(projetos);

        _mapperMock
            .Setup(mapper => mapper.Map<IEnumerable<ProjetoDTO>>(It.IsAny<List<Projeto>>()))
            .Returns(projetosDTO);

        // Act
        var result = await _controller.ObterTodos(page: 1, size: 10);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(projetos.Count, result.Total);
        Assert.Equal(10, result.Size);
        Assert.Equal(1, result.Page);
        Assert.Equal(projetos.Count, result.Items.Count());
    }

    [Fact]
    public async Task ObterPorId_DeveRetornarProjetoDTO()
    {
        // Arrange
        var projeto = new Projeto { Id = 1, Nome = "Projeto A" };
        var projetoDTO = new ProjetoDTO { Id = 1, Nome = "Projeto A" };

        _projetoServiceMock
            .Setup(service => service.ObterPorId(projeto.Id))
            .ReturnsAsync(projeto);

        _mapperMock
            .Setup(mapper => mapper.Map<ProjetoDTO>(projeto))
            .Returns(projetoDTO);

        // Act
        var result = await _controller.ObterPorId(1);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(projetoDTO.Id, result.Id);
        Assert.Equal(projetoDTO.Nome, result.Nome);
    }

    [Fact]
    public async Task Adicionar_DeveRetornarCreatedComProjeto()
    {
        // Arrange
        var projetoDTO = new ProjetoSemResponsavelDTO { Nome = "Projeto A" };
        var projeto = new Projeto { Id = 1, Nome = "Projeto A" };

        _mapperMock
            .Setup(mapper => mapper.Map<Projeto>(projetoDTO))
            .Returns(projeto);

        _projetoServiceMock
            .Setup(service => service.Adicionar(projeto))
            .Returns(Task.CompletedTask);

        // Act
        var result = await _controller.Adicionar(projetoDTO);
        var response = Assert.IsType<ObjectResult>(result.Result);

        // Assert
        Assert.Equal(201, response.StatusCode);
        Assert.Equal(projeto, response.Value);
    }

    [Fact]
    public async Task Atualizar_DeveRetornarOkComProjetoAtualizado()
    {
        // Arrange
        var projetoDTO = new ProjetoSemResponsavelDTO { Nome = "Projeto Atualizado" };
        var projeto = new Projeto { Id = 1, Nome = "Projeto Atualizado" };

        _mapperMock
            .Setup(mapper => mapper.Map<Projeto>(projetoDTO))
            .Returns(projeto);

        _projetoServiceMock
            .Setup(service => service.Atualizar(projeto))
            .Returns(Task.CompletedTask);

        // Act
        var result = await _controller.Atualizar(1, projetoDTO);
        var response = Assert.IsType<ObjectResult>(result);

        // Assert
        Assert.Equal(200, response.StatusCode);
        Assert.Equal(projeto, response.Value);
    }
   
    [Fact]
    public async Task VincularFuncionarios_DeveRetornarBadRequestSeIdsFuncionariosVazio()
    {
        // Arrange
        var projetoId = 1;
        var idsFuncionarios = "";

        // Act
        var result = await _controller.VincularFuncionarios(projetoId, idsFuncionarios);
        var response = Assert.IsType<BadRequestObjectResult>(result);

        // Assert
        Assert.Equal(400, response.StatusCode);
        Assert.Equal("É obrigatório selecionar funcionários para vincular.", response.Value);
    }
}
