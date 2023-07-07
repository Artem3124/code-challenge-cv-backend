using FileScopeProvider.Models;

namespace SolutionValidator.Python.Models
{
    public class PythonFile : SystemFile
    {
        public PythonFile(string content, string name) : base($"{name}.py", content)
        {

        }
    }
}
