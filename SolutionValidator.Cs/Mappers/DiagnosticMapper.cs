using Microsoft.CodeAnalysis;
using Shared.Core.Compilers;
using Shared.Core.Interfaces;

namespace SolutionValidator.Cs.Mappers
{
    public interface IDiagnosticMapper : IMapper<Diagnostic, CompilationDiagnostic>
    {

    }

    internal class DiagnosticMapper : IDiagnosticMapper
    {
        public CompilationDiagnostic Map(Diagnostic entity) => new(entity.GetMessage(), GetSeverity(entity), 0, 0);

        public List<CompilationDiagnostic> Map(List<Diagnostic> entity) => entity.Select(e => Map(e)).ToList();

        private DiagnosticSeverityInternal GetSeverity(Diagnostic entity) => entity.Severity switch
        {
            DiagnosticSeverity.Error => DiagnosticSeverityInternal.Error,
            DiagnosticSeverity.Warning => entity.IsWarningAsError ? DiagnosticSeverityInternal.Error : DiagnosticSeverityInternal.Warning,
            _ => DiagnosticSeverityInternal.Info,
        };
    }
}
