using CodeProblemAssistant.Data.Enums;
using Shared.Core.Enums;

namespace InternalTypes.Types
{
    internal class PythonTypeSet : TypeSetBase
    {
        public PythonTypeSet() : base(CodeLanguage.Python, new()
        {
            { InternalType._Int32, string.Empty },
            { InternalType._Int64, string.Empty },
            { InternalType._Float, string.Empty },
            { InternalType._Double, string.Empty },
            { InternalType._String, string.Empty },
            { InternalType._Char, string.Empty },
            { InternalType._Array, string.Empty },
            { InternalType._Bool, string.Empty },
        }) { }

        public override string TypeAsString(InternalType type)
        {
            return string.Empty;
        }

        public override string ValueOfTypeAsString(InternalType type, string value)
        {
            return type switch
            {
                InternalType._String => WrapString(value),
                InternalType._Char => WrapString(value),
                InternalType._Array => FormatArray(GetTypeOfArray(type), value),
                InternalType._Bool => string.Concat(value[0].ToString().ToUpper(), value.AsSpan(1)),
                _ => value,
            };
        }

        private string FormatArray(InternalType primitive, string value)
        {
            if (primitive is InternalType._String or InternalType._Char)
            {
                value = string.Join(",", value.Split(",").Select(v => ValueOfTypeAsString(primitive, v.Trim())));
            }
            return $"[{value}]";
        }

        private string WrapString(string value) => $"\"{value}\"";
    }
}
