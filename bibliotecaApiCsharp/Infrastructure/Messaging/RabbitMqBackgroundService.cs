using bibliotecaApiCsharp.Infrastructure.Messaging;

public class RabbitMqBackgroundService : BackgroundService
{
    private readonly AddLivroReceiver _receiver;

    public RabbitMqBackgroundService(AddLivroReceiver receiver)
    {
        _receiver = receiver;
    }

    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _receiver.StartListening();
        return Task.CompletedTask;
    }

    public override void Dispose()
    {
        _receiver.Dispose();
        base.Dispose();
    }
}