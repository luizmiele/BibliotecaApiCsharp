using bibliotecaApiCsharp.Application.DTO;
using bibliotecaApiCsharp.Application.Services;
using bibliotecaApiCsharp.Domain.Entities;
using bibliotecaApiCsharp.Domain.Repositories;
using bibliotecaApiCsharp.Infrastructure.Repositories;
using Moq;
using Xunit;

namespace bibliotecaApiCsharp.Tests;

public class LivroApplicationServiceTests
{
    private readonly Mock<ILivroRepository> _mockLivroRepository;
    private readonly Mock<IEmprestimoRepository> _mockEmprestimoRepository;
    private readonly Mock<IUsuarioRepository> _mockUsuarioRepository;
    private readonly Mock<IAddLivroProducer> _mockAddLivroProducer;
    private readonly LivroApplicationService _livroService;

    public LivroApplicationServiceTests()
    {
        _mockLivroRepository = new Mock<ILivroRepository>();
        _mockEmprestimoRepository = new Mock<IEmprestimoRepository>();
        _mockUsuarioRepository = new Mock<IUsuarioRepository>();
        _mockAddLivroProducer = new Mock<IAddLivroProducer>();

        _livroService = new LivroApplicationService(
            _mockLivroRepository.Object,
            _mockEmprestimoRepository.Object,
            _mockUsuarioRepository.Object,
            _mockAddLivroProducer.Object
        );
    }

    [Fact]
    public async Task AddLivroAsync_DeveAdicionarNovoLivro()
    {
        var livroCreateDTO = new LivroCreateDTO { Titulo = "Livro Teste", Autor = "Autor Teste" };

        _mockLivroRepository.Setup(r => r.AddLivroAsync(livroCreateDTO)).Returns(Task.CompletedTask);
        _mockLivroRepository.Setup(r => r.GetLivroByIdAsync(It.IsAny<int>()))
            .ReturnsAsync(new Livro { LivroId = 1, Titulo = "Livro Teste", Autor = "Autor Teste", EstaDisponivel = true });

        await _livroService.AddLivroAsync(livroCreateDTO);

        _mockLivroRepository.Verify(r => r.AddLivroAsync(livroCreateDTO), Times.Once);
        _mockAddLivroProducer.Verify(p => p.SendMsg("Livro Teste"), Times.Once);
    }

    [Fact]
    public async Task GetLivrosAsync_DeveRetornarTodosOsLivrosCadastrados()
    {
        var livros = new List<LivroDTO>
        {
            new LivroDTO(1, "Livro 1", "Autor 1", true, null),
            new LivroDTO(2, "Livro 2", "Autor 2", false, null)
        };

        _mockLivroRepository.Setup(r => r.GetAllLivrosAsync()).ReturnsAsync(livros);

        var result = await _livroService.GetLivrosAsync();

        _mockLivroRepository.Verify(r => r.GetAllLivrosAsync(), Times.Once);
        Assert.Equal(2, result.Count());
    }

    [Fact]
    public async Task EmprestimoLivroAsync_DeveRealizarEmprestimoDeLivro()
    {
        var livro = new Livro { LivroId = 1, Titulo = "Livro 1", Autor = "Autor 1", EstaDisponivel = true };
        var usuario = new UsuarioDTO { UsuarioId = 1, Nome = "Usuario Teste" };

        _mockLivroRepository.Setup(r => r.GetLivroByIdAsync(1)).ReturnsAsync(livro);
        _mockUsuarioRepository.Setup(r => r.GetUsuarioByIdAsync(1)).ReturnsAsync(usuario);
        _mockEmprestimoRepository.Setup(r => r.AddEmprestimoAsync(It.IsAny<Emprestimo>())).ReturnsAsync(1);

        var result = await _livroService.EmprestimoLivroAsync(1, 1);

        _mockLivroRepository.Verify(r => r.UpdateLivroAsync(It.Is<Livro>(l => l.LivroId == 1 && !l.EstaDisponivel && l.EmprestimoId == 1)), Times.Once);
        _mockEmprestimoRepository.Verify(r => r.AddEmprestimoAsync(It.IsAny<Emprestimo>()), Times.Once);
        _mockAddLivroProducer.Verify(p => p.SendLivroEmprestado("Livro 1"), Times.Once);

        Assert.Equal("Livro foi emprestado para: Usuario Teste", result);
    }

    [Fact]
    public async Task DevolveLivroAsync_DeveDevolverLivroEmprestado()
    {
        var livro = new Livro { LivroId = 1, Titulo = "Livro 1", Autor = "Autor 1", EstaDisponivel = false, EmprestimoId = 1 };
        var emprestimo = new EmprestimoDTO { EmprestimoId = 1, LivroId = 1, UsuarioId = 1, DataEmprestimo = DateTime.UtcNow.AddDays(-15), Status = "emprestado" };

        _mockLivroRepository.Setup(r => r.GetLivroByIdAsync(1)).ReturnsAsync(livro);
        _mockEmprestimoRepository.Setup(r => r.GetEmprestimoByLivroIdStatusAtivoAsync(1)).ReturnsAsync(emprestimo);

        var result = await _livroService.DevolveLivroAsync(1);

        _mockLivroRepository.Verify(r => r.UpdateLivroAsync(It.Is<Livro>(l => l.LivroId == 1 && l.EstaDisponivel && l.EmprestimoId == null)), Times.Once);
        _mockEmprestimoRepository.Verify(r => r.UpdateEmprestimoAsync(It.IsAny<EmprestimoDTO>(), 1), Times.Once);
        _mockAddLivroProducer.Verify(p => p.SendLivroDevolvido("Livro 1"), Times.Once);

        Assert.Equal("Livro Devolvido",result);
    }

    [Fact]
    public void AddLivroProducer_SendMessage_DeveEnviarMensagem()
    {
        _mockAddLivroProducer.Object.SendMsg("Teste de mensagem");

        _mockAddLivroProducer.Verify(p => p.SendMsg("Teste de mensagem"), Times.Once);
    }
}