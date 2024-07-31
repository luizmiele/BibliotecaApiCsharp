using bibliotecaApiCsharp.Application.DTO;
using bibliotecaApiCsharp.Domain.Services;
using Microsoft.AspNetCore.Mvc;

namespace bibliotecaApiCsharp.Presentation.Controllers;

[ApiController]
[Route("usuarios")]
public class UsuarioController : ControllerBase
{
    private readonly IUsuarioService _usuarioService;

    public UsuarioController(IUsuarioService usuarioService)
    {
        _usuarioService = usuarioService;
    }
    [HttpPost]
    public async Task<IActionResult> AddUsuario(UsuarioDTO usuarioDTO)
    {
        await _usuarioService.AddUsuarioAsync(usuarioDTO);
        return Ok("USUARIO CRIADO COM SUCESSO!");
    }

    [HttpGet]
    public async Task<IActionResult> GetUsuarios()
    {
        var usuarios = await _usuarioService.GetAllUsuariosAsync();
        return Ok(usuarios);
    }
    
    [HttpGet("{id}")]
    public async Task<IActionResult> GetUsuarioById(int id)
    {
        var usuario = await _usuarioService.GetUsuarioByIdAsync(id);
        return Ok(usuario);
    }
    
}