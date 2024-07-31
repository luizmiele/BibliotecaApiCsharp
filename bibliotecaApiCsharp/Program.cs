using bibliotecaApiCsharp.Application.Services;
using bibliotecaApiCsharp.Domain.Repositories;
using bibliotecaApiCsharp.Domain.Services;
using bibliotecaApiCsharp.Infrastructure.ConexaoDB;
using bibliotecaApiCsharp.Infrastructure.Config;
using bibliotecaApiCsharp.Infrastructure.Messaging;
using bibliotecaApiCsharp.Infrastructure.Repositories;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configure DbConnectionFactory
builder.Services.AddSingleton<DbConnection>(sp =>
{
    var configuration = sp.GetRequiredService<IConfiguration>();
    var connectionString = configuration.GetConnectionString("DefaultConnection");
    return new DbConnection(connectionString);
});

// Register repositories
builder.Services.AddScoped<ILivroRepository, LivroRepository>();
builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();
builder.Services.AddScoped<IEmprestimoRepository, EmprestimoRepository>();

// Register services
builder.Services.AddScoped<ILivroService, LivroApplicationService>();
builder.Services.AddScoped<IUsuarioService, UsuarioApplicationService>();
builder.Services.AddScoped<IEmprestimoService, EmprestimoApplicationService>();

// Register RabbitMQ configuration and services
builder.Services.Configure<RabbitMQConfig>(builder.Configuration.GetSection("RabbitMQConfig"));

// Register RabbitMQ consumers
new AddLivroReceiver();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
