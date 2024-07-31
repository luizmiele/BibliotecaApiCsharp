using bibliotecaApiCsharp.Domain.Services;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace bibliotecaApiCsharp.Presentation.Controllers;

[ApiController]
[Route("emprestimos")]
public class EmprestimoController : ControllerBase
{
    private readonly IEmprestimoService _emprestimoService;

    public EmprestimoController(IEmprestimoService emprestimoService)
    {
        _emprestimoService = emprestimoService;
    }
    [HttpGet]
    public async Task<IActionResult> GetEmprestimos()
    {
        var emprestimos = await _emprestimoService.GetAllEmprestimosAsync();
        return Ok(emprestimos);
    }
}