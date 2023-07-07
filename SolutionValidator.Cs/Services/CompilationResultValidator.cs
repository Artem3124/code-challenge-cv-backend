using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Emit;
using SolutionValidator.Cs.Exceptions;
using SolutionValidator.Cs.Interfaces;
using SolutionValidators.Core.Models;

namespace SolutionValidator.Cs.Services
{
    internal class CompilationResultValidator : ICompilationResultValidator
    {
        public void Validate(EmitResult result)
        {
            if (!result.Success)
            {
                throw new CSCompilationException(result.Diagnostics.ToList());
            }
        }

        private List<CompilationError> GetErrors(EmitResult result) => result.Diagnostics
            .Where(d => d.Severity == DiagnosticSeverity.Error || d.IsWarningAsError)
            .Select(d => new CompilationError(d.ToString()))
            .ToList();
    }
}
