using CppTestContextBuilder.Core.Models;
using Shared.Core.Extensions;

namespace Cpp.CompilerProxy.Models
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
