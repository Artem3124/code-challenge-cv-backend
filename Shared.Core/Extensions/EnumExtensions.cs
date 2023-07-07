using System.ComponentModel;

namespace Shared.Core.Extensions
{
    public static class EnumExtensions
    {
        public static string GetDescription(this Enum value)
        {
            if (value == null)
            {
                return string.Empty;
            }
            var attr = value.GetType()?.GetField(value.ToString())
                ?.GetCustomAttributes(typeof(DescriptionAttribute), false).FirstOrDefault() as DescriptionAttribute;

            return attr?.Description ?? string.Empty;
        }
    }
}
