using Shared.Core.Interfaces;
using Shared.Core.Models;
using SolutionValidator.Python.Interfaces;

namespace SolutionValidator.Python.Services
{
    public class PythonScriptRunner : IPythonScriptRunner
    {
        private readonly ISystemProcessService _systemProcessService;

        public PythonScriptRunner(ISystemProcessService systemProcessService)
        {
            _systemProcessService = systemProcessService;
        }

        public SystemProcessOutput Run(string fileName, bool failFast)
        {
            return _systemProcessService.Execute(new()
            {
                FileName = "python",
                RedirectStandardError = true,
                RedirectStandardOutput = true,
                UseShellExecute = false,
                Arguments = $"{GetFileName(fileName)} {FailFast(failFast)}",
            });
        }

        private string FailFast(bool failFast) => failFast ? "--failfast" : string.Empty;

        private string GetFileName(string value) => value.EndsWith(".py") ? value : $"{value}.py";
    }
}