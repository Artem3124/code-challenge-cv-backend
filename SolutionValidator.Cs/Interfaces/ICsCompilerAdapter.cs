using Shared.Core.Compilers;
using SolutionValidator.Cs.Models;

namespace SolutionValidator.Cs.Interfaces
{
    internal interface ICsCompilerAdapter
    {
        CompilationResult Compile(CompilationRequest request);
    }
}