using Shared.Core.Compilers;

namespace Cpp.CompilerProxy.Wrappers
{
    internal interface IAssemblyWrapper
    {
        IInternalAssembly Wrap(string executableName);
    }
}