using Shared.Core.Models;

namespace SolutionValidator.Python.Interfaces
{
    public interface IPythonScriptRunner
    {
        SystemProcessOutput Run(string fileName, bool failFast);
    }
}