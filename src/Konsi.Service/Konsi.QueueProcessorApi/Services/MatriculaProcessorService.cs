using Konsi.CrawlerApi.Interfaces.Services;
using Konsi.QueueProcessorApi.Interfaces.Services;

namespace Konsi.QueueProcessorApi.Services;

public class MatriculaProcessorService : IMatriculaProcessorService
{
    private readonly IQueueService _queueService;
    private readonly IRedisService _redisService;
    private readonly ICrawlerService _crawlerService;

    public MatriculaProcessorService(IQueueService queueService, IRedisService redisService, ICrawlerService crawlerService)
    {
        _queueService = queueService;
        _redisService = redisService;
        _crawlerService = crawlerService;
    }

    public void ProcessMatricula()
    {
        while (true)
        {
            var matricula = _queueService.DequeueMatricula();
            if (matricula == null)
            {
                // Nenhuma matrícula na fila, então podemos sair do loop
                break;
            }

            var matriculaData = _redisService.GetMatriculaData(matricula);
            if (matriculaData == null)
            {
                // Não há dados no Redis para esta matrícula, então precisamos executar o crawler
                matriculaData = _crawlerService.GetMatriculaData(matricula);
                _redisService.SetMatriculaData(matricula, matriculaData);
            }

            // Agora você tem os dados da matrícula, seja do Redis ou do crawler, e pode fazer o que precisar com eles
        }

    }
}
