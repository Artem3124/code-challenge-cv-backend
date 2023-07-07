using Shared.Core.Compilers;

namespace SolutionValidator.Cpp.Services
{
    internal interface IDiagnosticParser
    {
        List<CompilationDiagnostic> StringToDiagnostic(string value);
    }
}