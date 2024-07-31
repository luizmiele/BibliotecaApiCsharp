namespace bibliotecaApiCsharp.Infrastructure.Config;

public class RabbitMQConfig
{
    public string HostName { get; set; }
    public string UserName { get; set; }
    public string Password { get; set; }
    public string Exchange { get; set; }
    public string AddLivroQueue { get; set; } = "AddLivroQueue";
    public string EmprestaLivroQueue { get; set; } = "EmprestaLivroQueue";
    public string DevolveLivroQueue { get; set; } = "DevolveLivroQueue";
}