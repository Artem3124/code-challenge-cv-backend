using Mongo.Data.Models;

namespace Mongo.Data.Services
{
    public interface ITestCaseService
    {
        Task<Guid> CreateTestCaseSetAsync(List<TestCaseItem> testCases, CancellationToken cancellationToken = default);
        Task<List<TestCaseItem>> GetTestCasesAsync(Guid uuid, CancellationToken cancellationToken = default);
        Task CreateTestCaseSetAsync(List<TestCaseItem> testCases, Guid setUUID, CancellationToken cancellationToken = default);
    }
}