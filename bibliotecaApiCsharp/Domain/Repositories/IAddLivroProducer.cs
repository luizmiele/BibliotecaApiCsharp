namespace bibliotecaApiCsharp.Infrastructure.Repositories;

public interface IAddLivroProducer
{
     void SendMsg(string msg);
     void SendLivroEmprestado(string msg); 
     void SendLivroDevolvido(string msg);
}