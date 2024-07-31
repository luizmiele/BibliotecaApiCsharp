using bibliotecaApiCsharp.Application.DTO;
using bibliotecaApiCsharp.Domain.Repositories;
using bibliotecaApiCsharp.Domain.Services;

namespace bibliotecaApiCsharp.Application.Services;

public class EmprestimoApplicationService : IEmprestimoService
{
    private readonly IEmprestimoRepository _emprestimoRepository;
    
    public EmprestimoApplicationService( IEmprestimoRepository emprestimoRepository)
    {
        _emprestimoRepository = emprestimoRepository;
    }
    
    
    public async Task<EmprestimoDTO> GetEmprestimoById(int id)
    {
        var emprestimo = await _emprestimoRepository.GetEmprestimoByIdAsync(id);
        if (emprestimo == null)
            throw new Exception("Livro não encontrado com o id: " + id);
        return emprestimo;
    }

    public async Task<IEnumerable<EmprestimoDTO>> GetAllEmprestimosAsync()
    {
        var emprestimos = await _emprestimoRepository.GetAllEmprestimosAsync();
        return emprestimos.Select(emprestimo => new EmprestimoDTO(emprestimo.EmprestimoId, emprestimo.LivroId, emprestimo.UsuarioId,
            emprestimo.DataEmprestimo,emprestimo.DataDevolucao, emprestimo.Status));
    }
}