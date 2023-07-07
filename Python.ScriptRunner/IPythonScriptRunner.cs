using Shared.Core.Models;

namespace Python.ScriptRunner
{
    public interface IPythonScriptRunner
    {
        SystemProcessOutput Run(string fileName, bool failFast);
    }
}