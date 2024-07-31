namespace bibliotecaApiCsharp.Application.DTO;

public class EmprestimoCreateDTO
{
    public int LivroId { get; set; }
    public int UsuarioId { get; set; }
    public DateTime DataEmprestimo { get; set; }
    public DateTime? DataDevolucao { get; set; }
    public string Status { get; set; }

    
}