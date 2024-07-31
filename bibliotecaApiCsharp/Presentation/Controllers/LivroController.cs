using bibliotecaApiCsharp.Application.DTO;
using bibliotecaApiCsharp.Domain.Services;
using Microsoft.AspNetCore.Mvc;

namespace bibliotecaApiCsharp.Presentation.Controllers;

[ApiController]
[Route("livros")]
public class LivroController : ControllerBase
{
    private readonly ILivroService _livroService;

    public LivroController(ILivroService livroService)
    {
        _livroService = livroService;
    }

    [HttpPost]
    public async Task<IActionResult> AddLivro(LivroCreateDTO livroCreateDTO)
    {
        await _livroService.AddLivroAsync(livroCreateDTO);
        return Ok("LIVRO CRIADO COM SUCESSO!");
    }

    [HttpGet]
    public async Task<IActionResult> GetLivros()
    {
        var livros = await _livroService.GetLivrosAsync();
        return Ok(livros);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetLivro(int id)
    {
        var livro = await _livroService.GetLivroByIdAsync(id);
        if (livro == null)
            return NotFound();
        return Ok(livro);
    }

    [HttpPost("emprestar")]
    public async Task<IActionResult>EmprestimoLivro(int livroId, int userId)
    {
        await _livroService.EmprestimoLivroAsync(livroId, userId);
        return Ok();
    }

    [HttpPost("devolver")]
    public async Task<IActionResult>DevolveLivro(int livroId)
    {
        await _livroService.DevolveLivroAsync(livroId);
        return Ok();
    }
}
