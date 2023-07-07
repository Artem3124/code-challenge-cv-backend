using Shared.Core.Compilers;
using SolutionValidator.Cpp.Models;

namespace SolutionValidator.Cpp.Interfaces
{
    public interface ICppCompilerAdapter
    {
        CompilationResult Compile(CppCompilationRequest request);
    }
}