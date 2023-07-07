using CSCompiler.Models;
using NoName.Core.Extensions;
using System.Text;

namespace CSCompiler.Services
{
    internal class UsingService
    {
        // TODO: Probably change those so it won't change model props
        public CompilationContext AddUsingAndReference(CompilationContext compilation, CSUsing csUsing)
        {
            compilation.ThrowIfNull(nameof(compilation));
            csUsing.ThrowIfNull(nameof(csUsing));

            var builder = new StringBuilder(Using(csUsing.Namespace));
            builder.Append(compilation.SourceCode);

            compilation.SourceCode = builder.ToString();
            compilation.ReferenceLocations.Add(csUsing.ReferenceLocation);

            return compilation;
        }

        public CompilationContext AddUsingAndReference(CompilationContext compilation, List<CSUsing> csUsing)
        {
            compilation.ThrowIfNull(nameof(compilation));
            csUsing.ThrowIfNull(nameof(csUsing));

            if (csUsing.Count == 0)
            {
                return compilation;
            }

            var builder = new StringBuilder();
            csUsing.ForEach(u =>
            {
                builder.Append(Using(u.Namespace));

                compilation.ReferenceLocations.Add(u.ReferenceLocation);
            });
            builder.Append(compilation.SourceCode);
            
            compilation.SourceCode = builder.ToString() + compilation.SourceCode;

            return compilation;
        }

        private string Using(string name) => $"using {name};";
    }
}
