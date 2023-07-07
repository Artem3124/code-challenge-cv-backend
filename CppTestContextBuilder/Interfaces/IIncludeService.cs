using CppTestContextBuilder.Core.Models;

namespace CppTestContextBuilder.Interfaces
{
    internal interface IIncludeService
    {
        CppCompilationContext AddIncludes(CppCompilationContext context, string fileName, List<Include> includes);
    }
}