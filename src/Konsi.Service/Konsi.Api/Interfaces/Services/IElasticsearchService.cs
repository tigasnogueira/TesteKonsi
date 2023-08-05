namespace Konsi.Api.Interfaces.Services;

public interface IElasticsearchService
{
    void IndexMatriculaData(string cpf, string data);
    Task<string> GetMatriculaData(string cpf);
}
