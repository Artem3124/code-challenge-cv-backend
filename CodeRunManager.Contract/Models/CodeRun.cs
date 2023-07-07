using Shared.Core.Enums;

namespace CodeRunManager.Contract.Models
{
#nullable disable
    public class CodeRun
    {
        public int Id { get; set; }

        public Guid UUID { get; set; }

        public Guid UserReferenceUUID { get; set; }

        public Guid CodeProblemReferenceUUID { get; set; }

        public DateTime? StartedAtUtc { get; set; }

        public DateTime? CompletedAtUtc { get; set; }

        public CodeRunState State { get; set; }

        public PriorityType Priority { get; set; }

        public CodeLanguage CodeLanguageId { get; set; }

        public RunType RunTypeId { get; set; }

        public string SourceCode { get; set; }
    }
#nullable enable
}
