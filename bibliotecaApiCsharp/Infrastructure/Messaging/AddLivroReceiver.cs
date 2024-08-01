using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace bibliotecaApiCsharp.Infrastructure.Messaging
{
    public class AddLivroReceiver : IDisposable
    {
        private readonly IConnection _connection;
        private readonly IModel _channel;

        public AddLivroReceiver()
        {
            var factory = new ConnectionFactory { HostName = "localhost" };
            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();

            // Declarar as filas
            _channel.QueueDeclare(queue: "livro-criado",
                durable: true,
                exclusive: false,
                autoDelete: false,
                arguments: null);

            _channel.QueueDeclare(queue: "livro-emprestado",
                durable: true,
                exclusive: false,
                autoDelete: false,
                arguments: null);

            _channel.QueueDeclare(queue: "livro-devolvido",
                durable: true,
                exclusive: false,
                autoDelete: false,
                arguments: null);
        }

        public void StartListening()
        {
            var livroCriadoConsumer = new EventingBasicConsumer(_channel);
            livroCriadoConsumer.Received += (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                Console.WriteLine($"[x] RECEBIDO: Novo livro registrado com o Titulo: {message}");
            };
            _channel.BasicConsume(queue: "livro-criado",
                autoAck: true,
                consumer: livroCriadoConsumer);
            
            var livroEmprestadoConsumer = new EventingBasicConsumer(_channel);
            livroEmprestadoConsumer.Received += (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                Console.WriteLine($"[x] RECEBIDO: Livro emprestado: {message}");
            };
            _channel.BasicConsume(queue: "livro-emprestado",
                autoAck: true,
                consumer: livroEmprestadoConsumer);
            
            var livroDevolvidoConsumer = new EventingBasicConsumer(_channel);
            livroDevolvidoConsumer.Received += (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                Console.WriteLine($"[x] RECEBIDO: Livro devolvido: {message}");
            };
            _channel.BasicConsume(queue: "livro-devolvido",
                autoAck: true,
                consumer: livroDevolvidoConsumer);
        }

        public void Dispose()
        {
            _channel?.Close();
            _connection?.Close();
        }
    }
}
