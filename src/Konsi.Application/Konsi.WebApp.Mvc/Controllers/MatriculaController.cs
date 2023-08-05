using Konsi.Api.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace Konsi.WebApp.Mvc.Controllers;

public class MatriculaController : Controller
{
    private readonly IElasticsearchService _elasticsearchService;

    public MatriculaController(IElasticsearchService elasticsearchService)
    {
        _elasticsearchService = elasticsearchService;
    }

    public IActionResult Index()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Search(string cpf)
    {
        var data = await _elasticsearchService.GetMatriculaData(cpf);
        return View("Index", data);
    }
}
