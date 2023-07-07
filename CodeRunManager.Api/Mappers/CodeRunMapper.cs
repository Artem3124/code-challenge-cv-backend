using CodeRunManager.Contract.Models;
using Shared.Core.Interfaces;
using DataModels = CodeRunManager.Data.Models;

namespace CodeRunManager.Api.Mappers
{
    public interface ICodeRunMapper : IMapper<DataModels.CodeRun, CodeRun>
    {

    }

    public class CodeRunMapper : ICodeRunMapper
    {
        public CodeRun Map(Data.Models.CodeRun entity) => new()
        {
            Id = entity.Id,
            SourceCode = entity.SourceCode,
            State = entity.State,
            StartedAtUtc = entity.StartedAtUtc,
            CodeLanguageId = entity.CodeLanguageId,
            CompletedAtUtc = entity.CompletedAtUtc,
            RunTypeId = entity.RunTypeId,
            CodeProblemReferenceUUID = entity.CodeProblemReferenceUUID,
            Priority = entity.Priority,
            UserReferenceUUID = entity.UserReferenceUUID,
            UUID = entity.UUID,
        };

        public List<CodeRun> Map(List<Data.Models.CodeRun> entity) => entity.Select(e => Map(e)).ToList();
    }
}
