using bibliotecaApiCsharp.Application.DTO;

namespace bibliotecaApiCsharp.Domain.Entities;

public class Usuario
{
    public int UsuarioId { get; set; }
    public string Nome { get; set; }
    public string Email { get; set; }


    public Usuario()
    {
    }

    public Usuario(string nome, string email)
    {
        Nome = nome;
        Email = email;
    }

    public Usuario(int usuarioId, string nome, string email)
    {
        UsuarioId = usuarioId;
        Nome = nome;
        Email = email;
    }

    public Usuario(UsuarioCreateDTO usuarioCreateDto)
    {
        
    }
}