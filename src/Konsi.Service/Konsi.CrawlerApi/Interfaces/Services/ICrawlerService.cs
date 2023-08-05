namespace Konsi.CrawlerApi.Interfaces.Services;

public interface ICrawlerService
{
    Task<List<string>> GetMatriculas(string cpf, string username, string password);
}
