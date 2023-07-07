using CSCompiler.Contract.Models;
using Microsoft.CodeAnalysis;

namespace CSCompiler.Core.Exceptions
{
    internal class CSCompilationException : Exception
    {
        public List<Diagnostic> Diagnostics { get; init; }

        public CSCompilationException(List<Diagnostic> diagnostics)
            : base("One or more errors occurred while compiling the source code.")
        {
            Diagnostics = diagnostics;
        }
    }
}
