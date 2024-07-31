using System.Data;
using Npgsql;

namespace bibliotecaApiCsharp.Infrastructure.ConexaoDB;

public class DbConnection
{
    private readonly string _connectionString;

    public DbConnection(string connectionString)
    {
        _connectionString = connectionString;
    }

    public IDbConnection CreateConnection()
    {
        return new NpgsqlConnection(_connectionString);
    }
}