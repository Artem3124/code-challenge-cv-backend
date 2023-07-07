using CppTestContextBuilder.Core.Models;

namespace CppTestContextBuilder.Interfaces
{
    internal interface ICppIncludesProvider
    {
        List<Include> Get();
    }
}