using bibliotecaApiCsharp.Domain.Entities;
using bibliotecaApiCsharp.Application.DTO;
namespace bibliotecaApiCsharp.Domain.Services;

public interface IUsuarioService
{
    Task<string> AddUsuarioAsync(UsuarioDTO usuarioDTO);
    Task<IEnumerable<UsuarioDTO>> GetAllUsuariosAsync();
    Task<UsuarioDTO> GetUsuarioByIdAsync(int id);
}