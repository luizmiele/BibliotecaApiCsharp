using bibliotecaApiCsharp.Domain.Entities;
using bibliotecaApiCsharp.Application.DTO;


namespace bibliotecaApiCsharp.Domain.Repositories;

public interface IEmprestimoRepository
{
    Task<int> AddEmprestimoAsync(Emprestimo emprestimo);
    Task UpdateEmprestimoAsync(EmprestimoDTO emprestimoDTO, int livroId);
    Task<EmprestimoDTO> GetEmprestimoByLivroIdStatusAtivoAsync(int livroId);
    Task<EmprestimoDTO> GetEmprestimoByIdAsync(int id);

    Task<IEnumerable<EmprestimoDTO>> GetAllEmprestimosAsync();
}