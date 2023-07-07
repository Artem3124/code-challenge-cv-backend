using AccountManager.Contract;
using AccountManager.Data.Enum;
using CodeRunManager.Api.Interfaces;
using CodeRunManager.Api.Mappers;
using CodeRunManager.Contract.Models;
using CodeRunManager.Data;
using Mongo.Data.Services;
using Shared.Core.Enums;
using Shared.Core.Extensions;
using DataModels = CodeRunManager.Data.Models;

namespace CodeRunManager.Api.Services
{
    public class QueueService : IQueueService
    {
        private readonly CodeRunManagerContext _db;
        private readonly IAccountManagerClient _accountManagerClient;
        private readonly ICodeRunQueueMessageService _codeRunQueueMessageService;

        public QueueService(CodeRunManagerContext db, ICodeRunQueueMessageService codeRunQueueMessageService, ICodeRunMapper mapper, IAccountManagerClient accountManagerClient)
        {
            _db = db.ThrowIfNull();
            _accountManagerClient = accountManagerClient.ThrowIfNull();
            _codeRunQueueMessageService = codeRunQueueMessageService;
        }

        public async Task<Guid> EnqueueAsync(CodeRunQueueRequest request, CancellationToken cancellationToken = default)
        {
            var userSubscription = await _accountManagerClient.GetSubscriptionByUserUUIDAsync(request.UserReferenceUUID);

            var codeRun = new DataModels.CodeRun
            {
                UUID = Guid.NewGuid(),
                CodeLanguageId = request.CodeLanguageId,
                CodeProblemReferenceUUID = request.CodeProblemReferenceUUID,
                UserReferenceUUID = request.UserReferenceUUID,
                SourceCode = request.SourceCode,
                State = CodeRunState.Queued,
                RunTypeId = request.RunTypeId,
                CreatedAtUtc = DateTime.UtcNow,
                Priority = GetPriorityFromSubscription(userSubscription),
            };
            _db.CodeRuns.Add(codeRun);

            await _codeRunQueueMessageService.EnqueueAsync(new()
            {
                UserReferenceUUID = request.UserReferenceUUID,
                SourceCode = request.SourceCode,
                CodeLanguageId = request.CodeLanguageId,
                CodeProblemReferenceUUID = request.CodeProblemReferenceUUID,
                RunTypeId = request.RunTypeId,
                UUID = codeRun.UUID,
            }, GetPriorityFromSubscription(userSubscription), cancellationToken);
            await _db.SaveChangesAsync(cancellationToken);

            return codeRun.UUID;
        }

        private PriorityType GetPriorityFromSubscription(SubscriptionType subscription) => subscription switch
        {
            SubscriptionType.NoSubscription => PriorityType.Medium,
            SubscriptionType.Basic => PriorityType.High,
            _ => throw new NotImplementedException(),
        };
    }
}
