using Shared.Core.Enums;

namespace CodeRunManager.Contract.Models
{
    public class CodeRunResultQueryRequest
    {
        public Guid? UUID { get; set; }

        public Guid? CodeProblemUUID { get; set; }

        public CodeRunOutcome? Outcome { get; set; }

        public Guid? UserUUID { get; set; }

        public RunType? RunType { get; set; }

        public Guid? CodeRunUUID { get; set; }
    }
}
