using bibliotecaApiCsharp.Application.DTO;

namespace bibliotecaApiCsharp.Domain.Entities;

public class Livro
{
    public int LivroId { get; set; }
    public string Titulo { get; set; }
    public string Autor { get; set; }
    public bool EstaDisponivel { get; set; } = true;
        
    public int? EmprestimoId { get; set; }

    public Livro()
    {
    }

    public Livro(int livroId, string titulo, string autor, bool estaDisponivel, int? emprestimoId)
    {
        LivroId = livroId;
        Titulo = titulo;
        Autor = autor;
        EstaDisponivel = estaDisponivel;
        EmprestimoId = emprestimoId;
    }

    public Livro(LivroDTO livroDTO)
    {
        LivroId = livroDTO.LivroId;
        Titulo = livroDTO.Titulo;
        Autor = livroDTO.Autor;
        EstaDisponivel = livroDTO.EstaDisponivel;
        EmprestimoId = livroDTO.EmprestimoId;

    }

    public Livro(LivroCreateDTO livroCreateDTO)
    {
        Titulo = livroCreateDTO.Titulo;
        Autor = livroCreateDTO.Autor;
        EstaDisponivel = true;
    }
}