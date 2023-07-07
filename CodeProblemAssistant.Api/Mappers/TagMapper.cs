using CodeProblemAssistant.Contract.Models;
using Shared.Core.Interfaces;
using DataModels = CodeProblemAssistant.Data.Models;

namespace CodeProblemAssistant.Api.Mappers
{
    public interface ITagMapper : IMapper<DataModels.Tag, Tag>
    {

    }

    public class TagMapper : ITagMapper
    {
        public Tag Map(DataModels.Tag entity) => new()
        {
            Name = entity.Name,
            Id = entity.Id,
        };

        public List<Tag> Map(List<DataModels.Tag> entity) => entity.Select(e => Map(e)).ToList();
    }
}
