namespace SolutionValidator.Cpp.Models
{
    public class SourceFile : CppFile
    {
        public SourceFile(string name, string content) : base($"{name}.cpp", content)
        {

        }
    }
}
