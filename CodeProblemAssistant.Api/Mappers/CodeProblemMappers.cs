using CodeProblemAssistant.Contract.Models;
using Newtonsoft.Json;
using Shared.Core.Extensions;
using Shared.Core.Interfaces;
using DataModels = CodeProblemAssistant.Data.Models;

namespace CodeProblemAssistant.Api.Mappers
{
    public interface ICodeProblemMapper : IMapper<DataModels.CodeProblem, CodeProblem>
    {

    }

    public class CodeProblemMapper : ICodeProblemMapper
    {
        private readonly CodeProblemMethodInfoMapper _codeProblemMethodInfoMapper;
        private readonly ITagMapper _tagMapper;

        public CodeProblemMapper(CodeProblemMethodInfoMapper codeProblemMethodInfoMapper, ITagMapper tagMapper)
        {
            _codeProblemMethodInfoMapper = codeProblemMethodInfoMapper.ThrowIfNull();
            _tagMapper = tagMapper.ThrowIfNull();
        }

        public CodeProblem Map(DataModels.CodeProblem codeProblem) => new()
        {
            UUID = codeProblem.UUID,
            Description = codeProblem.Description,
            DownVotesCount = codeProblem.Votes.Count(v => !v.UpVote),
            UpVotesCount = codeProblem.Votes.Count(v => v.UpVote),
            ComplexityTypeId = codeProblem.ComplexityTypeId,
            Constraints = JsonConvert.DeserializeObject<List<string>>(codeProblem.ConstraintsJson),
            Examples = JsonConvert.DeserializeObject<List<Example>>(codeProblem.ExamplesJson),
            Name = codeProblem.Name,
            Tags = _tagMapper.Map(codeProblem.Tags),
            MethodInfo = _codeProblemMethodInfoMapper.Map(codeProblem),
        };

        public List<CodeProblem> Map(List<DataModels.CodeProblem> codeProblems) => codeProblems.Select(p => Map(p)).ToList();
    }
}
