using CodeRunManager.Contract.Models;
using Mongo.Data.Models;

namespace WorkerService.Stager.Interfaces;

public interface IStageService
{
    Task Start(CodeRunQueueMessage codeRun, CancellationToken cancellationToken = default);
}