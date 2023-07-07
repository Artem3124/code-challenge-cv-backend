using Microsoft.Extensions.Options;
using Mongo.Data.Models;
using Mongo.Data.Settings;
using MongoDB.Driver;

namespace Mongo.Data.Services
{
    internal class TestCaseService : ITestCaseService
    {
        private readonly IMongoCollection<TestCaseSet> _testCaseSetCollection;

        public TestCaseService(IOptions<CodeSkillsDatabaseSettings> options)
        {
            return;
            var mongoClient = new MongoClient(
                options.Value.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(
                options.Value.DatabaseName);

            _testCaseSetCollection = mongoDatabase.GetCollection<TestCaseSet>(
                options.Value.TestCaseSetCollectionName);
        }

        public async Task<List<TestCaseItem>> GetTestCasesAsync(Guid uuid, CancellationToken cancellationToken = default)
        {
            var testCaseSet = await _testCaseSetCollection.Find(t => t.UUID == uuid).FirstOrDefaultAsync(cancellationToken);

            return testCaseSet.TestCases;
        }

        public Task CreateTestCaseSetAsync(List<TestCaseItem> testCases, Guid setUUID, CancellationToken cancellationToken = default)
        {
            var existing = FindAsync(setUUID, cancellationToken);

            var testCaseSet = new TestCaseSet
            {
                Id = existing,
                UUID = setUUID,
                TestCases = testCases,
            };

            return existing != default
                ? _testCaseSetCollection.ReplaceOneAsync(t => t.UUID == setUUID, testCaseSet, cancellationToken: cancellationToken)
                : _testCaseSetCollection.InsertOneAsync(testCaseSet, cancellationToken: cancellationToken);
        }
           

        public async Task<Guid> CreateTestCaseSetAsync(List<TestCaseItem> testCases, CancellationToken cancellationToken = default)
        {
            var uuid = Guid.NewGuid();

            await _testCaseSetCollection.InsertOneAsync(new TestCaseSet
            {
                UUID = uuid,
                TestCases = testCases,
            }, cancellationToken: cancellationToken);

            return uuid;
        }

        private string FindAsync(Guid uuid, CancellationToken cancellationToken = default)
        {
            return _testCaseSetCollection.Find(t => t.UUID == uuid).FirstOrDefault(cancellationToken)?.Id;
        }
    }
}
