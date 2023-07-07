using CodeProblemAssistant.Contract.Models;
using TestCases.Models;

namespace CodeProblemAssistant.Api.Interfaces
{
    public interface ICodeProblemService
    {
        Task<CodeProblem> GetAsync(Guid codeProblemUUID, CancellationToken cancellationToken = default);

        Task<List<CodeProblem>> GetAllAsync(CancellationToken cancellationToken = default);

        Task<List<TestCase>> GetRunTestCaseSetAsync(Guid codeProblemUUID, CancellationToken cancellationToken = default);

        Task<List<TestCase>> GetSubmitTestCaseSetAsync(Guid codeProblemUUID, CancellationToken cancellationToken = default);

        Task<Guid> CreateAsync(CodeProblemCreateRequest request, CancellationToken cancellationToken = default);
    }
}