namespace Shared.Core.Enums
{
    public enum CodeRunOutcome
    {
        Succeeded = 0,
        RuntimeError = 1,
        TestFailed = 2,
        CompilationError = 3,
        TimeLimitExceeded = 4,
        MemoryLimitExceeded = 5,

        Unknown = 255
    }
}
