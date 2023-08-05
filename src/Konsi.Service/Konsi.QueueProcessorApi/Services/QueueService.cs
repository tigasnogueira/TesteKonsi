using Konsi.QueueProcessorApi.Interfaces.Services;
using RabbitMQ.Client;
using System.Text;

namespace Konsi.QueueProcessorApi.Services;

public class QueueService : IQueueService
{
    private readonly ConnectionFactory _factory;

    public QueueService(ConnectionFactory factory)
    {
        _factory = factory;
    }

    public string DequeueMatricula()
    {
        using (var connection = _factory.CreateConnection())
        using (var channel = connection.CreateModel())
        {
            channel.QueueDeclare(queue: "matriculas",
                                 durable: false,
                                 exclusive: false,
                                 autoDelete: false,
                                 arguments: null);

            var data = channel.BasicGet("matriculas", true);

            if (data == null)
                return null;

            var body = data.Body.ToArray();
            var matricula = Encoding.UTF8.GetString(body);

            return matricula;
        }
    }

    public void EnqueueMatriculas(List<string> matriculas)
    {
        using (var connection = _factory.CreateConnection())
        using (var channel = connection.CreateModel())
        {
            channel.QueueDeclare(queue: "matriculas",
                                 durable: false,
                                 exclusive: false,
                                 autoDelete: false,
                                 arguments: null);

            foreach (var matricula in matriculas)
            {
                var body = Encoding.UTF8.GetBytes(matricula);
                channel.BasicPublish(exchange: "",
                                     routingKey: "matriculas",
                                     basicProperties: null,
                                     body: body);
            }
        }
    }
}
