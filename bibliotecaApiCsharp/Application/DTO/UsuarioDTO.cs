namespace bibliotecaApiCsharp.Application.DTO;

public class UsuarioDTO
{
    public int UsuarioId { get; set; }
    public string Nome { get; set; }
    public string Email { get; set; }

    public UsuarioDTO()
    {
        
    }
    
    public UsuarioDTO(int usuarioId, string nome, string email)
    {
        UsuarioId = usuarioId;
        Nome = nome;
        Email = email;
    }
}