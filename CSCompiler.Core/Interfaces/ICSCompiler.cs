using CSCompiler.Contract.Models;
using CSCompiler.Core.Models;
using Shared.Core.Compilers;
using System.Reflection;

namespace CSCompiler.Core.Interfaces
{
    internal interface ICSCompiler
    {
        CompilationResult Compile(CompilationRequest request);
    }
}