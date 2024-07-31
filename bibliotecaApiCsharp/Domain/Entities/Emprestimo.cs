namespace bibliotecaApiCsharp.Domain.Entities;

public class Emprestimo
{
    public int EmprestimoId { get; set; }
    public int LivroId { get; set; }
    public int UsuarioId { get; set; }
    public DateTime DataEmprestimo { get; set; }
    public DateTime DataDevolucao { get; set; }
    public string Status { get; set; }

    public Emprestimo()
    {
    }

    public Emprestimo(int livroId, int usuarioId)
    {
        LivroId = livroId;
        UsuarioId = usuarioId;
        DataEmprestimo = DateTime.Now;
        DataDevolucao = DateTime.Now.AddDays(7);
        Status = "ATIVO";
    }
}