using Microsoft.CodeAnalysis.Emit;

namespace CSCompiler.Core.Interfaces
{
    internal interface ICompilationResultValidator
    {
        void Validate(EmitResult result);
    }
}