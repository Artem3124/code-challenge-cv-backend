using FileScopeProvider.Models;

namespace Python.TestContextBuilder.Models
{
    public class PythonFile : SystemFile
    {
        public PythonFile(string content, string name) : base($"{name}.py", content)
        {

        }
    }
}
