using Microsoft.CodeAnalysis;
using System.Reflection;

namespace CSCompiler.Extensions
{
    internal static class TypeExtensions
    {
        public static string GetAssemblyLocation(this Type type) => type.GetTypeInfo().Assembly.Location;
    }
}
