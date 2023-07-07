using CodeProblemAssistant.Data.Enums;
using Shared.Core.Enums;

namespace CodeProblemAssistant.Contract.Models
{
    public class ChallengeCreateRequest
    {
        public Guid HostUUID { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public double TimeLimitMinutes { get; set; }

        public DateTime EndDateTimeUtc { get; set; }

        public bool IsPrivate { get; set; }

        public List<CodeLanguage> AllowedLanguages { get; set; } = new();

        public List<Guid>? AllowedUsers { get; set; }

        public bool AllowInvalidSyntaxSubmit { get; set; }

        public CodeProblemMethodInfo MethodInfo { get; set; }
    }

    public class Challenge
    {
        public Guid UUID { get; set; }

        public CodeProblemMethodInfo MethodInfo { get; set; }

        public Guid HostUUID { get; set; }

        public string Name { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public double TimeLimitMinutes { get; set; }

        public DateTime EndDateTimeUtc { get; set; }

        public List<CodeLanguage> AllowedLanguages { get; set; } = new();

        public ChallengeAttempt? UserAttempt { get; set; }

        public bool IsPrivate { get; set; }

        public List<Guid> AllowedUsers { get; set; } = new();

        public List<ChallengeAttempt> Attempts { get; set; } = new();

        public bool AllowInvalidSyntaxSubmission { get; set; }

        public Guid? TestCaseReferenceUUID { get; set; }
    }

    public class ChallengeUpdateRequest
    {
        public ChallengeSubmitState State { get; set; }
    }

    public class ChallengeStartRequest
    {
        public Guid UserUUID { get; set; }

        public Guid ChallengeUUID { get; set; }
    }

    public class ChallengeSubmitRequest
    {
        public Guid UserUUID { get; set; }

        public Guid ChallengeUUID { get; set; }

        public string SourceCode { get; set; }

        public CodeLanguage CodeLanguage { get; set; }

    }

    public class ChallengeAttempt
    {
        public Guid UUID { get; set; }

        public Guid UserUUID { get; set; }

        public ChallengeSubmitState State { get; set; }

        public CodeLanguage CodeLanguage { get; set; }

        public string SourceCode { get; set; }

        public DateTime StartedDateTimeUtc { get; set; }

        public DateTime? SubmittedDateTimeUtc { get; set; }
    }
}
