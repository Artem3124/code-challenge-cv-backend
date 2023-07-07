using CSCompiler.Contract.Models;
using Shared.Core.Compilers;

namespace CSCompiler.Contract.Interfaces
{
    public interface ICSCompilerClient
    {
        CompilationResult Compile(CSCompilationRequest request);
    }
}