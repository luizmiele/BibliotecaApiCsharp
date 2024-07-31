using bibliotecaApiCsharp.Domain.Entities;

namespace bibliotecaApiCsharp.Application.DTO;

public class LivroDTO
{
    public int LivroId { get; set; }
    public string Titulo { get; set; }
    public string Autor { get; set; }
    public bool EstaDisponivel { get; set; }
    
    public int? EmprestimoId { get; set; }

    public LivroDTO()
    {
    }

    public LivroDTO(Livro livro)
    {
        LivroId = livro.LivroId;
        Titulo = livro.Titulo;
        Autor = livro.Autor;
        EstaDisponivel = livro.EstaDisponivel;
        EmprestimoId = livro.EmprestimoId;
    }

    public LivroDTO(int livroId, string titulo, string autor, bool estaDisponivel, int? emprestimoId)
    {
        LivroId = livroId;
        Titulo = titulo;
        Autor = autor;
        EstaDisponivel = estaDisponivel;
        EmprestimoId = emprestimoId;
    }
}