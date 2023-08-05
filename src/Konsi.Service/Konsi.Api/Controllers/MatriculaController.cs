using Konsi.Api.Dtos;
using Konsi.CrawlerApi.Interfaces.Services;
using Konsi.QueueProcessorApi.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace Konsi.Api.Controllers;

public class MatriculaController : Controller
{
    private readonly ICrawlerService _crawlerService;
    private readonly IQueueService _queueService;
    private readonly IRedisService _redisService;

    public MatriculaController(ICrawlerService crawlerService, IQueueService queueService, IRedisService redisService)
    {
        _crawlerService = crawlerService;
        _queueService = queueService;
        _redisService = redisService;
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] MatriculaRequest request)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        // Execute o crawler e adicione os números de matrícula à fila
        var matriculas = await _crawlerService.GetMatriculas(request.Cpf, request.Username, request.Password);
        _queueService.EnqueueMatriculas(matriculas);

        return Ok();
    }

    [HttpGet("{cpf}")]
    public IActionResult Get(string cpf)
    {
        // Obtenha os dados do crawler para o CPF do Redis
        var matriculaData = _redisService.GetMatriculaData(cpf);
        if (matriculaData == null)
        {
            return NotFound();
        }

        return Ok(matriculaData);
    }
}
