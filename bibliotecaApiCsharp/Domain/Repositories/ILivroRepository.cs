using bibliotecaApiCsharp.Domain.Entities;
using bibliotecaApiCsharp.Application.DTO;

namespace bibliotecaApiCsharp.Domain.Repositories;

public interface ILivroRepository
{
    Task AddLivroAsync(LivroCreateDTO livroCreateDTO);
    Task<IEnumerable<LivroDTO>> GetAllLivrosAsync();
    Task<Livro> GetLivroByIdAsync(int id);
    Task UpdateLivroAsync(Livro livro);
    Task DeleteLivroAsync(int id);
}