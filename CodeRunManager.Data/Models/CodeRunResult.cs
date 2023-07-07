using Shared.Core.Enums;
using Shared.Core.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace CodeRunManager.Data.Models
{
    public class CodeRunResult : Entity
    {
        [ForeignKey(nameof(CodeRun))]
        public int CodeRunId { get; set; }

        public CodeRun CodeRun { get; set; }

        [AllowNull]
        public string Metadata { get; set; }

        public RunType RunTypeId { get; set; }

        public CodeRunOutcome CodeRunOutcomeId { get; set; }
    }
}
