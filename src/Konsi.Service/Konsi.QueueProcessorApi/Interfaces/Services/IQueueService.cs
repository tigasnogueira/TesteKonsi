namespace Konsi.QueueProcessorApi.Interfaces.Services;

public interface IQueueService
{
    void EnqueueMatriculas(List<string> matriculas);
    string DequeueMatricula();
}
