using Shared.Core.Enums;
using Shared.Core.Models;

namespace CodeRunManager.Data.Models
{
    public class CodeRun : Entity
    {
        public Guid UserReferenceUUID { get; set; }

        public Guid CodeProblemReferenceUUID { get; set; }

        public DateTime? StartedAtUtc { get; set; }

        public DateTime? CompletedAtUtc { get; set; }

        public CodeRunState State { get; set; }

        public CodeRunStage Stage { get; set; }

        public int? CodeRunResultId { get; set; }

        public CodeRunResult CodeRunResult { get; set; }

        public PriorityType Priority { get; set; }

        public CodeLanguage CodeLanguageId { get; set; }

        public RunType RunTypeId { get; set; }

        public string SourceCode { get; set; }
    }
}
