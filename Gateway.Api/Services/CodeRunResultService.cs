using CodeRunManager.Contract.Interfaces;
using CodeRunManager.Contract.Models;
using Gateway.Api.Interfaces;
using Shared.Core.Exceptions;
using Shared.Core.Extensions;

namespace Gateway.Api.Services
{
    public class CodeRunResultService : ICodeRunResultService
    {
        private readonly ICodeRunManagerClient _codeRunManagerClient;

        public CodeRunResultService(ICodeRunManagerClient codeRunManagerClient)
        {
            _codeRunManagerClient = codeRunManagerClient.ThrowIfNull();
        }

        public Task<List<CodeRunResultExpanded>> GetByCodeProblemUUIDAsync(Guid userUUID, Guid codeProblemUUID, CancellationToken cancellationToken = default)
        {
            var request = BuildRequest(userUUID: userUUID, codeProblemUUID: codeProblemUUID);

            return _codeRunManagerClient.QueryCodeRunsExpandedAsync(request, cancellationToken);
        }

        public async Task<CodeRunResultExpanded> GetByUUIDAsync(Guid userUUID, Guid codeRunResultUUID, CancellationToken cancellationToken = default)
        {
            var request = BuildRequest(userUUID, uuid: codeRunResultUUID);

            var results = await _codeRunManagerClient.QueryCodeRunsExpandedAsync(request, cancellationToken);
            var codeRunResult = results.FirstOrDefault();
            if (codeRunResult == null)
            {
                throw new ObjectNotFoundException(codeRunResultUUID, nameof(codeRunResult));
            }

            return codeRunResult;
        }

        public Task<List<CodeRunResultExpanded>> GetByUserUUIDAsync(Guid userUUID, CancellationToken cancellationToken = default)
        {
            var request = BuildRequest(userUUID);

            return _codeRunManagerClient.QueryCodeRunsExpandedAsync(request, cancellationToken);
        }

        public async Task<CodeRunResultExpanded> GetByCodeCodeRunUUIDAsync(Guid userUUID, Guid codeRunUUID, CancellationToken cancellationToken = default)
        {
            var request = BuildRequest(userUUID, codeRunUUID: codeRunUUID);

            var results = await _codeRunManagerClient.QueryCodeRunsExpandedAsync(request, cancellationToken);
            var codeRunResult = results.FirstOrDefault();
            if (codeRunResult == null)
            {
                throw new ObjectNotFoundException(codeRunUUID, nameof(codeRunResult));
            }

            return codeRunResult;
        }

        private CodeRunResultQueryRequest BuildRequest(Guid userUUID, Guid? uuid = default, Guid? codeProblemUUID = default, Guid? codeRunUUID = default) => new()
        {
            CodeProblemUUID = codeProblemUUID,
            UserUUID = userUUID,
            UUID = uuid,
            CodeRunUUID = codeRunUUID,
        };
    }
}
