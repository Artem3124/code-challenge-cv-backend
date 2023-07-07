namespace CppTestContextBuilder.Core.Models
{
    public class HeaderFile : CppFile
    {
        public HeaderFile(string name, string content) : base($"{name}.h", content)
        {

        }
    }
}
