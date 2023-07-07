using CodeProblemAssistant.Contract.Models;
using Shared.Core.Interfaces;
using DataModels = CodeProblemAssistant.Data.Models;

namespace CodeProblemAssistant.Api.Mappers
{
    public interface IVoteMapper : IMapper<DataModels.Vote, Vote>
    {

    }

    public class VoteMapper : IVoteMapper
    {
        public Vote Map(DataModels.Vote entity) => new()
        {
            CodeProblemUUID = entity.CodeProblemUUID,
            UpVote = entity.UpVote,
        };

        public List<Vote> Map(List<DataModels.Vote> entity) =>
            entity.Select(e => Map(e)).ToList();
    }
}
