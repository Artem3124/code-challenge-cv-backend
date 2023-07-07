using CodeProblemAssistant.Data.Enums;
using Microsoft.EntityFrameworkCore;
using Shared.Core.Enums;
using Shared.Core.Models;

namespace CodeProblemAssistant.Data.Models
{
    public class Challenge : Entity
    {
        public Guid HostUUID { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public double TimeLimitMinutes { get; set; }

        public DateTime EndDateTimeUtc { get; set; }

        public string AllowedLanguagesCsv { get; set; }

        public bool IsPrivate { get; set; }

        public List<PrivateChallengeAllowedUsers> AllowedUsers { get; set; } = new();

        public List<ChallengeAttempt> Attempts { get; set; } = new();

        public bool AllowInvalidSyntaxSubmission { get; set; }

        public string ParameterNames { get; set; }

        public string ParameterTypes { get; set; }

        public InternalType ReturnType { get; set; }

        public Guid? TestCaseReferenceUUID { get; set; }
    }

    public class PrivateChallengeAllowedUsers
    {
        public int Id { get; set; }
        public int ChallengeId { get; set; }

        public Challenge Challenge { get; set; }

        public Guid UserReferenceUUID { get; set; }
    }

    public class ChallengeAttempt : Entity
    {
        public Guid UserUUID { get; set; }

        public ChallengeSubmitState State { get; set; }

        public CodeLanguage CodeLanguage { get; set; }

        public string SourceCode { get; set; }

        public DateTime StartDateTimeUtc { get; set; }

        public DateTime? SubmittedDateTimeUtc { get; set; }
    }
}
