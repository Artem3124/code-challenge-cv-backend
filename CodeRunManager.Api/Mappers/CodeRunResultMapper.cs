using DataModel = CodeRunManager.Data.Models;
using Shared.Core.Interfaces;
using CodeRunManager.Contract.Models;
using Newtonsoft.Json;
using Shared.Core.Extensions;
using TestCases.Models;
using Shared.Core.Compilers;
using SolutionValidators.Core.Models;

namespace CodeRunManager.Api.Mappers
{
    public interface ICodeRunResultMapper : IMapper<DataModel.CodeRunResult, CodeRunResult>
    {

    }

    public class CodeRunResultMapper : ICodeRunResultMapper
    {
        public CodeRunResult Map(DataModel.CodeRunResult entity)
        {
            var result = new CodeRunResult
            {
                CodeRunOutcomeId = entity.CodeRunOutcomeId,
                UUID = entity.UUID,
                CompletedAtUtc = entity.CreatedAtUtc,
            };

            return PopulateWithMetadata(entity.Metadata ?? string.Empty, result);
        }

        public List<CodeRunResult> Map(List<DataModel.CodeRunResult> entity)
        {
            return entity.Select(e => Map(e)).ToList();
        }

        private CodeRunResult PopulateWithMetadata(string metadataString, CodeRunResult model)
        {
            var metadata = JsonConvert.DeserializeObject<Dictionary<string, string>>(metadataString);

            if (metadata == null)
            {
                return model;
            }

            if (metadata.TryGetValue(MetadataEntryNames.CompilationErrors, out var compilationErrors))
            {
                var errors = JsonConvert.DeserializeObject<List<CompilationDiagnostic>>(compilationErrors);
                model.CompilationErrors = errors.Select(e => new CompilationError($"{e.Line}:{e.Column}:{e.Message}")).ToList();
            }
            if (metadata.TryGetValue(MetadataEntryNames.Duration, out var duration))
            {
                model.Duration = JsonConvert.DeserializeObject<float>(duration);
            }
            if (metadata.TryGetValue(MetadataEntryNames.FailedTestCase, out var testCase))
            {
                model.FailedTest = JsonConvert.DeserializeObject<TestCaseResult>(testCase);
            }

            return model;
        }
    }
}
