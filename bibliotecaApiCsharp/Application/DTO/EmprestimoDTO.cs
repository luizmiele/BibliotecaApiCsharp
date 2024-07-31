using bibliotecaApiCsharp.Domain.Entities;

namespace bibliotecaApiCsharp.Application.DTO;

public class EmprestimoDTO
{
    public int EmprestimoId { get; set; }
    public int LivroId { get; set; }
    public int UsuarioId { get; set; }
    public DateTime DataEmprestimo { get; set; }
    public DateTime? DataDevolucao { get; set; }
    public string Status { get; set; }

    public EmprestimoDTO()
    {
    }

    public EmprestimoDTO(Emprestimo emprestimo)
    {
        EmprestimoId = emprestimo.EmprestimoId;
        LivroId = emprestimo.LivroId;
        UsuarioId = emprestimo.UsuarioId;
        DataEmprestimo = emprestimo.DataEmprestimo;
        DataDevolucao = emprestimo.DataDevolucao;
        Status = emprestimo.Status;
    }
    
    public EmprestimoDTO(int emprestimoId, int livroId, int usuarioId, DateTime dataEmprestimo,
        DateTime? dataDevolucao, string status)
    {
        EmprestimoId = emprestimoId;
        LivroId = livroId;
        UsuarioId = usuarioId;
        DataEmprestimo = dataEmprestimo;
        DataDevolucao = dataDevolucao;
        Status = status;
    }
}