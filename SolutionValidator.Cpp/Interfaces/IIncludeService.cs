using SolutionValidator.Cpp.Models;

namespace SolutionValidator.Cpp.Interfaces
{
    internal interface IIncludeService
    {
        CppCompilationContext AddIncludes(CppCompilationContext context, string fileName, List<Include> includes);
    }
}