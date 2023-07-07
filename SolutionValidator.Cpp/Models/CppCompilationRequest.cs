using Shared.Core.Extensions;

namespace SolutionValidator.Cpp.Models
{
    public class CppCompilationRequest
    {
        public string Name { get; set; }

        public CppCompilationContext Context { get; set; }

        public CppCompilationRequest(string name, CppCompilationContext context)
        {
            Name = name;
            Context = context.ThrowIfNull();
        }
    }
}
