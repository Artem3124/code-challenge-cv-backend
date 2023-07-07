using Shared.Core.Compilers;

namespace Cpp.CompilerProxy.Services
{
    internal interface ICompilationDiagnosticCheckService
    {
        List<CompilationDiagnostic> MarkWarningsAsErrors(List<CompilationDiagnostic> diagnostics);
    }
}