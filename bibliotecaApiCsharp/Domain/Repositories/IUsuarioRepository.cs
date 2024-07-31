using bibliotecaApiCsharp.Application.DTO;
using bibliotecaApiCsharp.Domain.Entities;

namespace bibliotecaApiCsharp.Domain.Repositories;

public interface IUsuarioRepository
{
    Task AddUsuarioAsync(UsuarioDTO usuarioDTO);
    Task<IEnumerable<UsuarioDTO>> GetAllUsuariosAsync();
    Task<UsuarioDTO> GetUsuarioByIdAsync(int id);
}