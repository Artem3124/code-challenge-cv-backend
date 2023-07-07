namespace CppTestContextBuilder.Core.Models
{
    public class SourceFile : CppFile
    {
        public SourceFile(string name, string content) : base($"{name}.cpp", content)
        {

        }
    }
}
