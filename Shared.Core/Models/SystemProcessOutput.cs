namespace Shared.Core.Models
{
    public class SystemProcessOutput
    {
        public int ExitCode { get; set; }

        public string? StandardError { get; set; }

        public int? StandardOutput { get; set; }

        public SystemProcessOutput(int exitCode, string? standardError, int? standardOutput)
        {
            ExitCode = exitCode;
            StandardError = standardError;
            StandardOutput = standardOutput;
        }
    }
}
