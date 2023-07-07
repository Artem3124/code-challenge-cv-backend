using Shared.Core.Compilers;

namespace SolutionValidator.Cpp.Interfaces
{
    internal interface ICompilationDiagnosticCheckService
    {
        List<CompilationDiagnostic> MarkWarningsAsErrors(List<CompilationDiagnostic> diagnostics);
    }
}