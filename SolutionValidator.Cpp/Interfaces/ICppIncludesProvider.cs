using SolutionValidator.Cpp.Models;

namespace SolutionValidator.Cpp.Interfaces
{
    internal interface ICppIncludesProvider
    {
        List<Include> Get();
    }
}