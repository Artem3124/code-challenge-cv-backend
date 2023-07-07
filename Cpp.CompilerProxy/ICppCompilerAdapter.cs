using Cpp.CompilerProxy.Models;
using Shared.Core.Compilers;

namespace Cpp.CompilerProxy
{
    public interface ICppCompilerAdapter
    {
        CompilationResult Compile(CppCompilationRequest request);
    }
}