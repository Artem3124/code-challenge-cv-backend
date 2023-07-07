using Shared.Core.Interfaces;
using Shared.Core.Models;
using System.Diagnostics;

namespace SolutionValidators.Core.Services
{
    public class SystemProcessService : ISystemProcessService
    {
        public SystemProcessOutput Execute(ProcessStartInfo processStartInfo)
        {
            using var process = Process.Start(processStartInfo);
            if (process == null)
            {
                throw new Exception("Unable to start process.");
            }

            var errorOutput = processStartInfo.RedirectStandardError
                ? process.StandardError.ReadToEnd()
                : default;
            var output = processStartInfo.RedirectStandardOutput
                ? process.StandardOutput.Read()
                : default;

            process.WaitForExit();

            return new SystemProcessOutput(process.ExitCode, errorOutput, output);
        }
    }
}
