using Dapper;
using bibliotecaApiCsharp.Application.DTO;
using bibliotecaApiCsharp.Domain.Repositories;
using bibliotecaApiCsharp.Infrastructure.ConexaoDB;
using bibliotecaApiCsharp.Domain.Entities;

namespace bibliotecaApiCsharp.Infrastructure.Repositories;

public class LivroRepository : ILivroRepository
{
    //TODO: TESTAR SE OS CAMPOS DO BD PRECISAM SER IGUAIS DA ENTIDADE. SIM PRECISAM (AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA)

    private readonly DbConnection _dbConnection;

    public LivroRepository(DbConnection dbConnection)
    {
        _dbConnection = dbConnection;
    }

    public async Task AddLivroAsync(LivroCreateDTO livroCreateDTO)
    {
        using (var dbConnection = _dbConnection.CreateConnection())
        {
            var sqlQuery =
                "INSERT INTO Livros (titulo, autor, estaDisponivel) VALUES (@Titulo, @Autor, true)"; 
            await dbConnection.ExecuteAsync(sqlQuery, livroCreateDTO);
            
        }
    }

    public async Task<IEnumerable<LivroDTO>> GetAllLivrosAsync()
    {
        using (var dbConnection = _dbConnection.CreateConnection())
        {
            var sqlQuery = "SELECT * FROM Livros";
            return await dbConnection.QueryAsync<LivroDTO>(sqlQuery);
        }
    }

    public async Task<Livro> GetLivroByIdAsync(int id)
    {
        using (var dbConnection = _dbConnection.CreateConnection())
        {
            var sqlQuery = "SELECT * FROM Livros WHERE livroId = @Id";
            return await dbConnection.QueryFirstOrDefaultAsync<Livro>(sqlQuery, new {Id = id});
        }
    }

    public async Task UpdateLivroAsync(Livro livro)
    {
        using (var dbConnection = _dbConnection.CreateConnection())
        {
            var sqlQuery = "UPDATE Livros SET titulo = @Titulo, autor = @Autor, estaDisponivel = @EstaDisponivel, " +
                           " emprestimoId = @EmprestimoId WHERE livroId = @LivroId";
            await dbConnection.ExecuteAsync(sqlQuery, livro);
        }
    }

    public async Task DeleteLivroAsync(int id)
    {
        using (var dbConnection = _dbConnection.CreateConnection())
        {
            var sqlQuery = "DELETE FROM Livros WHERE livroId = @Id";
            await dbConnection.ExecuteAsync(sqlQuery, new {Id = id});
        }
    }
}