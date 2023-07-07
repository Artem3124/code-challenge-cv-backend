using System.Runtime.CompilerServices;

namespace Shared.Core.Extensions
{
    public static class NullCheckExtension
    {
        public static T ThrowIfNull<T>(this T obj, [CallerArgumentExpression("obj")] string? argumentName = default)
        {
            if (obj is null)
            {
                throw new ArgumentNullException(argumentName);
            }

            return obj;
        }
    }
}
