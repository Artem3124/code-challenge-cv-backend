using Microsoft.CodeAnalysis.Emit;

namespace SolutionValidator.Cs.Interfaces
{
    internal interface ICompilationResultValidator
    {
        void Validate(EmitResult result);
    }
}