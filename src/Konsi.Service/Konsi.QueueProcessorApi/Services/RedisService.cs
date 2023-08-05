using Konsi.QueueProcessorApi.Interfaces.Services;
using StackExchange.Redis;

namespace Konsi.QueueProcessorApi.Services;

public class RedisService : IRedisService
{
    private readonly ConnectionMultiplexer _redis;

    public RedisService(string connectionString)
    {
        _redis = ConnectionMultiplexer.Connect(connectionString);
    }

    public string GetMatriculaData(string matricula)
    {
        var db = _redis.GetDatabase();
        return db.StringGet(matricula);
    }

    public void SetMatriculaData(string matricula, string data)
    {
        var db = _redis.GetDatabase();
        db.StringSet(matricula, data);
    }
}
