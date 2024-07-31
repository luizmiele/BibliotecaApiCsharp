using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace bibliotecaApiCsharp.Infrastructure.Messaging;

public class AddLivroReceiver
{
   public AddLivroReceiver()
   {
      var factory = new ConnectionFactory { HostName = "localhost" };
      using var connection = factory.CreateConnection();
      using var channel = connection.CreateModel();
      
      channel.QueueDeclare(queue: "livro-criado",
         durable: true,
         exclusive: false,
         autoDelete: false,
         arguments: null);
      
      var consumer = new EventingBasicConsumer(channel);
      consumer.Received += (model, ea) =>
      {
         var body = ea.Body.ToArray();
         var message = Encoding.UTF8.GetString(body);
         Console.WriteLine($" [x] Novo livro registrado com o Titulo:  {message}");
      };
      channel.BasicConsume(queue: "livro-criado",
         autoAck: true,
         consumer: consumer);
   }
}