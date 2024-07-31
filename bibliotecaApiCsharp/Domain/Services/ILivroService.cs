using bibliotecaApiCsharp.Domain.Entities;
using bibliotecaApiCsharp.Application.DTO;

namespace bibliotecaApiCsharp.Domain.Services;

public interface ILivroService
{
    Task AddLivroAsync(LivroCreateDTO livroCreateDTO);
    Task<IEnumerable<LivroDTO>> GetLivrosAsync();
    Task<LivroDTO> GetLivroByIdAsync(int id);
    Task<string> EmprestimoLivroAsync(int livroId, int usuarioId);
    Task<string> DevolveLivroAsync(int livroId);
}