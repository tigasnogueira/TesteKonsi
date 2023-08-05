using HtmlAgilityPack;
using Konsi.CrawlerApi.Interfaces.Services;

namespace Konsi.CrawlerApi.Services;

public class CrawlerService : ICrawlerService
{
    private readonly HttpClient _httpClient;

    public CrawlerService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }
    
    public async Task<List<string>> GetMatriculas(string cpf, string username, string password)
    {
        // Fazer login no site
        var loginUrl = "http://example.com/login";
        var loginContent = new FormUrlEncodedContent(new[]
        {
            new KeyValuePair<string, string>("username", username),
            new KeyValuePair<string, string>("password", password)
        });
        var loginResponse = await _httpClient.PostAsync(loginUrl, loginContent);
        loginResponse.EnsureSuccessStatusCode();

        // Navegar até a página de consulta de CPF
        var consultaUrl = "http://example.com/consulta";
        var consultaContent = new FormUrlEncodedContent(new[]
        {
            new KeyValuePair<string, string>("cpf", cpf)
        });
        var consultaResponse = await _httpClient.PostAsync(consultaUrl, consultaContent);
        consultaResponse.EnsureSuccessStatusCode();

        // Fazer o parsing da página de resposta
        var htmlDocument = new HtmlDocument();
        htmlDocument.LoadHtml(await consultaResponse.Content.ReadAsStringAsync());

        // Extrair os números de matrícula
        var matriculas = new List<string>();
        var matriculaNodes = htmlDocument.DocumentNode.SelectNodes("//div[@class='matricula']");
        foreach (var matriculaNode in matriculaNodes)
        {
            matriculas.Add(matriculaNode.InnerText);
        }

        return matriculas;
    }
}
