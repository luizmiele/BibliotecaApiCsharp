using bibliotecaApiCsharp.Application.DTO;
using bibliotecaApiCsharp.Domain.Entities;

namespace bibliotecaApiCsharp.Domain.Services;

public interface IEmprestimoService
{ 
    Task<EmprestimoDTO> GetEmprestimoById(int id);
    Task<IEnumerable<EmprestimoDTO>> GetAllEmprestimosAsync();
}