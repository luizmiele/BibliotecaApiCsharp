using bibliotecaApiCsharp.Application.DTO;
using bibliotecaApiCsharp.Domain.Entities;
using bibliotecaApiCsharp.Domain.Repositories;
using bibliotecaApiCsharp.Infrastructure.ConexaoDB;
using Dapper;

namespace bibliotecaApiCsharp.Infrastructure.Repositories;

public class UsuarioRepository : IUsuarioRepository
{
    private readonly DbConnection _dbConnection;

    public UsuarioRepository(DbConnection dbConnection)
    {
        _dbConnection = dbConnection;
    }
    public async Task AddUsuarioAsync(UsuarioDTO usuarioDTO)
    {
        using (var dbConnection = _dbConnection.CreateConnection())
        {
            var sqlQuery = "INSERT INTO Usuarios (nome, email) VALUES (@nome, @email)";
            await dbConnection.ExecuteAsync(sqlQuery, usuarioDTO);
        }
    }

    public async Task<IEnumerable<UsuarioDTO>> GetAllUsuariosAsync()
    {
        using (var dbConnection = _dbConnection.CreateConnection())
        {
            var sqlQuery = "SELECT * FROM Usuarios";
            return await dbConnection.QueryAsync<UsuarioDTO>(sqlQuery);
        }
    }

    public async Task<UsuarioDTO> GetUsuarioByIdAsync(int id)
    {
        using (var dbConnection = _dbConnection.CreateConnection())
        {
            var sqlQuery = "SELECT * FROM Usuarios WHERE usuarioId = @id";
            return await dbConnection.QueryFirstOrDefaultAsync<UsuarioDTO>(sqlQuery, new { Id = id });
        }
    }
}

