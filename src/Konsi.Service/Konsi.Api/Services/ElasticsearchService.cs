using Elasticsearch.Net;
using Konsi.Api.Interfaces.Services;
using Nest;

public class ElasticsearchService : IElasticsearchService
{
    private readonly ElasticClient _client;

    public ElasticsearchService(string url)
    {
        var settings = new ConnectionSettings(new Uri(url));
        _client = new ElasticClient(settings);
    }

    public void IndexMatriculaData(string cpf, string data)
    {
        var response = _client.Index(data, i => i
            .Index("matriculas")
            .Id(cpf)
        );

        if (!response.IsValid)
        {
            throw new Exception("Erro ao indexar dados no Elasticsearch");
        }
    }

    public Task<string> GetMatriculaData(string cpf)
    {
        var response = _client.Get(DocumentPath<string>.Id(cpf), g => g.Index("matriculas"));

        if (!response.IsValid)
        {
            throw new Exception("Erro ao buscar dados no Elasticsearch");
        }

        return Task.FromResult(response.Source.ToString());
    }
}
