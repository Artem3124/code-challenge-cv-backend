using Microsoft.CodeAnalysis;
using System.Reflection;

namespace SolutionValidator.Cs.Extensions
{
    // TODO: Move from this assembly
    internal static class TypeExtensions
    {
        public static string GetAssemblyLocation(this Type type) => type.GetTypeInfo().Assembly.Location;
    }
}
