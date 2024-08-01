using bibliotecaApiCsharp.Application.DTO;
using bibliotecaApiCsharp.Domain.Entities;
using bibliotecaApiCsharp.Domain.Repositories;
using bibliotecaApiCsharp.Domain.Services;
using bibliotecaApiCsharp.Infrastructure.Repositories;
using Microsoft.Extensions.Options;

namespace bibliotecaApiCsharp.Application.Services;

public class LivroApplicationService : ILivroService
{
    private readonly ILivroRepository _livroRepository;
    private readonly IAddLivroProducer _addLivroProducer;
    private readonly IEmprestimoRepository _emprestimoRepository;
    private readonly IUsuarioRepository _usuarioRepository;
    public LivroApplicationService(ILivroRepository livroRepository, IEmprestimoRepository emprestimoRepository, IUsuarioRepository usuarioRepository, IAddLivroProducer addLivroProducer)
    {
        _livroRepository = livroRepository;
        _emprestimoRepository = emprestimoRepository;
        _usuarioRepository = usuarioRepository;
        _addLivroProducer = addLivroProducer;
    }
    

    
    
    public async Task AddLivroAsync(LivroCreateDTO livroCreateDTO)
    {
        var livro = new Livro(livroCreateDTO);
        livro.EstaDisponivel = true;
        await _livroRepository.AddLivroAsync(livroCreateDTO);
        _addLivroProducer.SendMsg(livroCreateDTO.Titulo);
    }

    public async Task<IEnumerable<LivroDTO>> GetLivrosAsync()
    {
        var livros = await _livroRepository.GetAllLivrosAsync();
        return livros.Select(livro => new LivroDTO(livro.LivroId, livro.Titulo,livro.Autor,livro.EstaDisponivel, livro.EmprestimoId));
    }

    public async Task<LivroDTO> GetLivroByIdAsync(int id)
    {
        var livro = await _livroRepository.GetLivroByIdAsync(id);
        if (livro == null)
            throw new Exception("Livro não encontrado com o id: " + id);
        return new LivroDTO(livro);
    }

    public async Task<string> EmprestimoLivroAsync(int livroId, int usuarioId)
    {
        var livro = await _livroRepository.GetLivroByIdAsync(livroId);
        var usuario = await _usuarioRepository.GetUsuarioByIdAsync(usuarioId);

        if (livro == null || !livro.EstaDisponivel)
            throw new Exception("Livro não disponivel");
        
        Emprestimo emprestimo = new Emprestimo(livroId, usuarioId);
        int emprestimoId = await _emprestimoRepository.AddEmprestimoAsync(emprestimo);
        emprestimo.EmprestimoId = emprestimoId;
        
        
        livro.EstaDisponivel = false;
        livro.EmprestimoId = emprestimo.EmprestimoId;
        _livroRepository.UpdateLivroAsync(livro);
        _addLivroProducer.SendLivroEmprestado(livro.Titulo);
        return "Livro foi emprestado para: " + usuario.Nome;
    }

    public async Task<string> DevolveLivroAsync(int livroId)
    {
        var livro = await _livroRepository.GetLivroByIdAsync(livroId);

        if (livro == null || livro.EstaDisponivel)
            throw new Exception("Livro está disponivel.");

        livro.EstaDisponivel = true;
        livro.EmprestimoId = null;
        await _livroRepository.UpdateLivroAsync(livro);

        var emprestimoDTO = await _emprestimoRepository.GetEmprestimoByLivroIdStatusAtivoAsync(livroId);
        Console.WriteLine(emprestimoDTO.Status);
        if (DateTime.Now > emprestimoDTO.DataDevolucao)
        {
            emprestimoDTO.Status = "Devolvido com atraso";
            await _emprestimoRepository.UpdateEmprestimoAsync(emprestimoDTO, livroId);
        }
        else
        {
            emprestimoDTO.Status = "Devolvido";
            await _emprestimoRepository.UpdateEmprestimoAsync(emprestimoDTO, livroId);
        }
        Console.WriteLine(emprestimoDTO.Status);
        _addLivroProducer.SendLivroDevolvido(livro.Titulo);
        return "Livro"  + emprestimoDTO.Status;
    }
}