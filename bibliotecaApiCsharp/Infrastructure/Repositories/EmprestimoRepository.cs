using bibliotecaApiCsharp.Application.DTO;
using bibliotecaApiCsharp.Domain.Entities;
using bibliotecaApiCsharp.Domain.Repositories;
using bibliotecaApiCsharp.Infrastructure.ConexaoDB;
using Dapper;

namespace bibliotecaApiCsharp.Infrastructure.Repositories;

public class EmprestimoRepository : IEmprestimoRepository
{
    private readonly DbConnection _dbConnection;

    public EmprestimoRepository(DbConnection dbConnection)
    {
        _dbConnection = dbConnection;
    }
    //TODO: FAZER TRATAMENTO DE ERRO AQUI?!
    public async Task<int> AddEmprestimoAsync(Emprestimo emprestimo)
    {
        using (var dbConnection = _dbConnection.CreateConnection())
        {
            var sqlQuery = "INSERT INTO Emprestimos (livroId, usuarioId, dataEmprestimo, dataDevolucao, status) " +
                           "VALUES (@LivroId, @UsuarioId, @DataEmprestimo, @DataDevolucao, @Status) RETURNING emprestimoId";
            return await dbConnection.ExecuteScalarAsync<int>(sqlQuery, emprestimo);
        } 
    }
    
    public async Task<EmprestimoDTO> GetEmprestimoByLivroIdStatusAtivoAsync(int livroId)
    {
        using (var dbConnection = _dbConnection.CreateConnection())
        {
            var sqlQuery = "SELECT * FROM Emprestimos WHERE livroId = @livroId AND status = 'ATIVO'";
            return await dbConnection.QueryFirstOrDefaultAsync<EmprestimoDTO>(sqlQuery, new{LivroId = livroId});
        }
    }

    public async Task<EmprestimoDTO> GetEmprestimoByIdAsync(int id)
    {
        using (var dbConnection = _dbConnection.CreateConnection())
        {
            var sqlQuery = "SELECT * FROM Emprestimos WHERE emprestimoId = @Id";
            return await dbConnection.QueryFirstOrDefaultAsync<EmprestimoDTO>(sqlQuery, new { Id = id });
        }
    }
    
    public async Task UpdateEmprestimoAsync(EmprestimoDTO emprestimoDTO, int livroId)
    {
        using (var dbConnection = _dbConnection.CreateConnection())
        {
            var sqlQuery = "UPDATE Emprestimos SET livroId = @LivroId, usuarioId = @UsuarioId,dataEmprestimo = @DataEmprestimo, " +
                           "dataDevolucao = @DataDevolucao, status = @Status WHERE livroId = @livroId AND emprestimoId = @EmprestimoId";
            await dbConnection.ExecuteAsync(sqlQuery, emprestimoDTO);
        }
    }

    public async Task<IEnumerable<EmprestimoDTO>> GetAllEmprestimosAsync()
    {
        using (var dbConnection = _dbConnection.CreateConnection())
        {
            var sqlQuery = "SELECT * FROM Emprestimos";
            return await dbConnection.QueryAsync<EmprestimoDTO>(sqlQuery);
        }
    }
}