namespace bibliotecaApiCsharp.Application.DTO;

public class LivroCreateDTO
{
    public string Titulo { get; set; }
    public string Autor { get; set; }

    public LivroCreateDTO()
    {
    }

    public LivroCreateDTO(string titulo, string autor)
    {
        Titulo = titulo;
        Autor = autor;
    }
}