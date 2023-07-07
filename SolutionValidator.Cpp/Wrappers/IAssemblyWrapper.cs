using Shared.Core.Compilers;

namespace SolutionValidator.Cpp.Wrappers
{
    internal interface IAssemblyWrapper
    {
        IInternalAssembly Wrap(string executableName);
    }
}