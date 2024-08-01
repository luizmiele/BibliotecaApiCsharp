using System.Text;
using bibliotecaApiCsharp.Infrastructure.Repositories;
using RabbitMQ.Client;

namespace bibliotecaApiCsharp.Infrastructure.Messaging;

public class AddLivroProducer : IAddLivroProducer
{
    private readonly IConnection _connection;
    private readonly IModel _channel;

    public AddLivroProducer()
    {
        var factory = new ConnectionFactory { HostName = "localhost" };
        _connection = factory.CreateConnection();
        _channel = _connection.CreateModel();
    }

    public void SendMsg(string message)
    {
        var body = Encoding.UTF8.GetBytes(message);
        _channel.BasicPublish(exchange: "",
            routingKey: "livro-criado",
            basicProperties: null,
            body: body);
        Console.WriteLine($"[x] Enviado livro criado: {message}");
    }

    public void SendLivroEmprestado(string message)
    {
        var body = Encoding.UTF8.GetBytes(message);
        _channel.BasicPublish(exchange: "",
            routingKey: "livro-emprestado",
            basicProperties: null,
            body: body);
        Console.WriteLine($"[x] Enviado livro emprestado: {message}");
    }

    public void SendLivroDevolvido(string message)
    {
        var body = Encoding.UTF8.GetBytes(message);
        _channel.BasicPublish(exchange: "",
            routingKey: "livro-devolvido",
            basicProperties: null,
            body: body);
        Console.WriteLine($"[x] Enviado livro devolvido: {message}");
    }

    public void Dispose()
    {
        _channel?.Close();
        _connection?.Close();
    }
}

