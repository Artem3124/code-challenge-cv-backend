using Shared.Core.Models;

namespace CodeProblemAssistant.Data.Models
{
    public class Comment : Entity
    {
        public string Message { get; set; }

        public Guid UserReferenceUUID { get; set; }

        public int CodeProblemId { get; set; }

        public int UpVotesCount { get; set; }

        public int DownVotesCount { get; set; }

        public CodeProblem CodeProblem { get; set; }
    }
}
