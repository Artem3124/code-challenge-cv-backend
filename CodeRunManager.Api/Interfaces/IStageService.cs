using CodeRunManager.Contract.Models;

namespace CodeRunManager.Api.Interfaces;

public interface IStageService
{
    Task Start(CodeRun codeRun, CancellationToken cancellationToken = default);
}