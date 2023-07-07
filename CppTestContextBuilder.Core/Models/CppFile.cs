using FileScopeProvider.Models;

namespace CppTestContextBuilder.Core.Models
{
    public abstract class CppFile : SystemFile
    {
        public CppFile(string name, string content) : base(name, content)
        {

        }
    }
}
