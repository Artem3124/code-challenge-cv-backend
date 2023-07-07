using Gateway.Contact.Models;
using Shared.Core.Interfaces;
using Assistant = CodeProblemAssistant.Contract.Models;

namespace Gateway.Api.Mappers
{
    public interface ICodeProblemMapper : IMapper<Assistant.CodeProblem, CodeProblem>
    {

    }

    public class CodeProblemMapper : ICodeProblemMapper
    {
        public CodeProblem Map(Assistant.CodeProblem entity)
        {
            return new CodeProblem
            {
                UUID = entity.UUID,
                Name = entity.Name,
                Constraints = entity.Constraints,
                ComplexityTypeId = entity.ComplexityTypeId,
                DownVotesCount = entity.DownVotesCount,
                UpVotesCount = entity.UpVotesCount,
                Description = entity.Description,
                Tags = entity.Tags.Select(t => t.Name).ToList(),
                Examples = entity.Examples,
            };
        }

        public List<CodeProblem> Map(List<Assistant.CodeProblem> entity)
        {
            return entity.Select(e => Map(e)).ToList();
        }

        public List<string> GetListFromCsv(string csv) =>
            csv.Split(',').ToList();
    }
}
