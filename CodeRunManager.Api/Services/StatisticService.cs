using CodeProblemAssistant.Contract.Clients;
using CodeProblemAssistant.Data.Enums;
using CodeRunManager.Api.Interfaces;
using CodeRunManager.Contract.Models;
using CodeRunManager.Data;
using Shared.Core.Enums;
using Shared.Core.Extensions;

namespace CodeRunManager.Api.Services
{
    public class StatisticService : IStatisticService
    {
        private readonly CodeRunManagerContext _db;
        private readonly ICodeProblemAssistantClient _codeProblemAssistantClient;

        public StatisticService(CodeRunManagerContext db, ICodeProblemAssistantClient codeProblemAssistantClient)
        {
            _db = db.ThrowIfNull();
            _codeProblemAssistantClient = codeProblemAssistantClient.ThrowIfNull();
        }

        public async Task<UserStatistic> GetStatisticForUser(Guid userUUID)
        {
            var codeProblems = await _codeProblemAssistantClient.GetCodeProblemsAsync();
            var codeProblemsByComplexityCount = new Dictionary<CodeProblemComplexity, int>()
            {
                { CodeProblemComplexity.Easy, 0 },
                { CodeProblemComplexity.Medium, 0 },
                { CodeProblemComplexity.Hard, 0 },
            };

            var distinctSolvedProblems = _db.CodeRuns
                .Where(c => c.UserReferenceUUID == userUUID && c.CodeRunResult.CodeRunOutcomeId == CodeRunOutcome.Succeeded)
                .ToList()
                .DistinctBy(c => c.CodeProblemReferenceUUID)
                .ToDictionary(c => c.CodeProblemReferenceUUID);

            foreach (var codeProblem in codeProblems)
            {
                if (distinctSolvedProblems.ContainsKey(codeProblem.UUID))
                {
                    codeProblemsByComplexityCount[codeProblem.ComplexityTypeId]++;
                }
            }

            return new UserStatistic
            {
                ProblemsCount = codeProblems.Count,
                EasyProblemsSolved = codeProblemsByComplexityCount[CodeProblemComplexity.Easy],
                MediumProblemsSolved = codeProblemsByComplexityCount[CodeProblemComplexity.Medium],
                HardProblemsSolved = codeProblemsByComplexityCount[CodeProblemComplexity.Hard],
            };
        }
    }
}
