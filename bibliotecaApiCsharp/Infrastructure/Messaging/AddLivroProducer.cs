

using System.Text;
using bibliotecaApiCsharp.Application.DTO;
using bibliotecaApiCsharp.Infrastructure.Config;
using Microsoft.Extensions.Options;

using RabbitMQ.Client;


namespace bibliotecaApiCsharp.Infrastructure.Messaging;

public class AddLivroProducer
{
    
    public AddLivroProducer()
    {
    }

    public void sendMsg(string titulo)
    {
        var factory = new ConnectionFactory { HostName = "localhost" };
        using var connection = factory.CreateConnection();
        using var channel = connection.CreateModel();
        
        channel.QueueDeclare(queue: "livro-criado",
            durable: true,
            exclusive: false,
            autoDelete: false,
            arguments: null);
        
        var body = Encoding.UTF8.GetBytes(titulo);
        channel.BasicPublish(exchange: string.Empty,
            routingKey: "livro-criado",
            basicProperties: null,
            body: body);
    }
}
