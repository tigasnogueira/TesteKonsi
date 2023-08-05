namespace Konsi.QueueProcessorApi.Interfaces.Services;

public interface IRedisService
{
    string GetMatriculaData(string matricula);
    void SetMatriculaData(string matricula, string data);
}
