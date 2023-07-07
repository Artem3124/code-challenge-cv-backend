using Shared.Core.Compilers;
using SolutionValidator.Cs.Models;

namespace SolutionValidator.Cs.Interfaces
{
    public interface ICsCompilerClient
    {
        CompilationResult Compile(CSCompilationRequest request);
    }
}