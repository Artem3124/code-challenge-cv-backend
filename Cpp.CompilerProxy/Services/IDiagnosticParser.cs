using Shared.Core.Compilers;

namespace Cpp.CompilerProxy.Services
{
    internal interface IDiagnosticParser
    {
        List<CompilationDiagnostic> StringToDiagnostic(string value);
    }
}