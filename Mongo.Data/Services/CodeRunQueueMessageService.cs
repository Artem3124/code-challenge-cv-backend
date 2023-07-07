using Microsoft.Extensions.Options;
using Mongo.Data.Models;
using Mongo.Data.Settings;
using MongoDB.Driver;
using Shared.Core.Enums;
using System.Threading;

namespace Mongo.Data.Services
{
    internal class CodeRunQueueMessageService : ICodeRunQueueMessageService
    {
        private readonly IMongoCollection<CodeRunQueueMessage> _codeRunQueueMessageCollectionHighPriority;
        private readonly IMongoCollection<CodeRunQueueMessage> _codeRunQueueMessageCollection;


        public CodeRunQueueMessageService(
            IOptions<CodeSkillsDatabaseSettings> settings)
        {
            var mongoClient = new MongoClient(
                settings.Value.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(
                settings.Value.DatabaseName);

            _codeRunQueueMessageCollection = mongoDatabase.GetCollection<CodeRunQueueMessage>(
                settings.Value.CodeRunMessageQueueName);

            _codeRunQueueMessageCollectionHighPriority = mongoDatabase.GetCollection<CodeRunQueueMessage>(
                $"{settings.Value.CodeRunMessageQueueName}HighPrior");
        }

        public async Task<CodeRunQueueMessage> DequeueAsync(CancellationToken cancellationToken = default)
        {
            var message = await FirstOrDefaultAsync(_codeRunQueueMessageCollectionHighPriority, cancellationToken) ??
                await FirstOrDefaultAsync(_codeRunQueueMessageCollection, cancellationToken);

            return message;
        }

        public async Task EnqueueAsync(CodeRunQueueMessage message, PriorityType priority, CancellationToken cancellationToken = default) 
        {
            switch (priority)
            {
                case PriorityType.High:
                    await _codeRunQueueMessageCollectionHighPriority.InsertOneAsync(message, cancellationToken: cancellationToken);
                    break;
                default:
                    await _codeRunQueueMessageCollection.InsertOneAsync(message, cancellationToken: cancellationToken);
                    break;
            }
        }

        private Task<CodeRunQueueMessage> FirstOrDefaultAsync(IMongoCollection<CodeRunQueueMessage> collection, CancellationToken cancellationToken = default) =>
            collection.FindOneAndDeleteAsync(_ => true, cancellationToken: cancellationToken);
    }
}
