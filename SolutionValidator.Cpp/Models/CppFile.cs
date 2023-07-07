using FileScopeProvider.Models;

namespace SolutionValidator.Cpp.Models
{
    public abstract class CppFile : SystemFile
    {
        public CppFile(string name, string content) : base(name, content)
        {

        }
    }
}
