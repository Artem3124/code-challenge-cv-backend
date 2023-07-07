using CSCompiler.Models;
using System.Reflection;

namespace CSCompiler.Interfaces
{
    internal interface ICSCompiler
    {
        Assembly Compile(CSCompilationRequest request);
    }
}