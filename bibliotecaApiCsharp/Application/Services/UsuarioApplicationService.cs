using bibliotecaApiCsharp.Domain.Repositories;
using bibliotecaApiCsharp.Domain.Services;
using bibliotecaApiCsharp.Application.DTO;
using bibliotecaApiCsharp.Domain.Entities;

namespace bibliotecaApiCsharp.Application.Services;

public class UsuarioApplicationService : IUsuarioService
{
    private readonly IUsuarioRepository _usuarioRepository;

    public UsuarioApplicationService(IUsuarioRepository usuarioRepository)
    {
        _usuarioRepository = usuarioRepository;
    }

    public async Task<string> AddUsuarioAsync(UsuarioDTO usuarioDTO)
    {
        Usuario usuario = new Usuario(usuarioDTO.Nome, usuarioDTO.Email);

        await _usuarioRepository.AddUsuarioAsync(usuarioDTO);
        return "Usuario cadastro com sucesso";
    }

    public async Task<IEnumerable<UsuarioDTO>> GetAllUsuariosAsync()
    {
        var usuarios = await _usuarioRepository.GetAllUsuariosAsync();
        return usuarios.Select(usuario => new UsuarioDTO(usuario.UsuarioId, usuario.Nome, usuario.Email));
    }

    public async Task<UsuarioDTO> GetUsuarioByIdAsync(int id)
    {
        var usuario = await _usuarioRepository.GetUsuarioByIdAsync(id);
        if (usuario == null)
            throw new Exception("Usuario não encontrado para o id " + id);
        return usuario;
    }
}