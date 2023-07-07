using CodeProblemAssistant.Data.Enums;
using Shared.Core.Enums;

namespace InternalTypes.Types
{
    internal class CsTypeSet : TypeSetBase
    {
        public CsTypeSet() : base(CodeLanguage.Cs, new()
        {
            { InternalType._Int32, "int" },
            { InternalType._Int64, "long" },
            { InternalType._Float, "float" },
            { InternalType._Double, "double" },
            { InternalType._String, "string" },
            { InternalType._Char, "char" },
            { InternalType._Array, "{0}[]" },
            { InternalType._Bool, "bool" },
        })
        { }

        public override string TypeAsString(InternalType type)
        {
            if (IsArray(type))
            {
                var arrayType = GetTypeOfArray(type);
                var typeOfArray = GetTypeOrThrow(arrayType);

                var value = string.Format(_types[InternalType._Array], typeOfArray);
                if (IsArray(arrayType))
                {
                    arrayType = GetTypeOfArray(arrayType);
                    value = String.Format(value, GetTypeOrThrow(arrayType));
                }

                return value;
            }

            return GetTypeOrThrow(type);
        }

        public override string ValueOfTypeAsString(InternalType type, string value)
        {
            if (IsArray(type))
            {
                return FormatArray(GetTypeOfArray(type), value);
            }

            return type switch
            {
                InternalType._String => $"\"{value}\"",
                InternalType._Char => $"\'{value}\'",
                _ => value,
            };
        }

        private string FormatArray(InternalType primitive, string value)
        {
            if (primitive is InternalType._String or InternalType._Char)
            {
                value = string.Join(",", value.Split(",").Select(v => ValueOfTypeAsString(primitive, v.Trim())));
            }
            if (IsArray(primitive))
            {
                return ValueOfTypeAsString(primitive, value);
            }
            return $"new {GetTypeOrThrow(primitive)}[] {{{value}}}";
        }

        private string GetTypeOrThrow(InternalType primitive)
        {
            if (!_types.TryGetValue(primitive, out var value))
            {
                throw new Exception("Unsupported");
            }

            return value;
        }
    }
}
