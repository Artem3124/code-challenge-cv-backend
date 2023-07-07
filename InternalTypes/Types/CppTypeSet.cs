using CodeProblemAssistant.Data.Enums;
using Shared.Core.Enums;

namespace InternalTypes.Types;

internal class CppTypeSet : TypeSetBase
{
    public CppTypeSet() : base(CodeLanguage.Cpp, new()
    {
        { InternalType._Int32, "int" },
        { InternalType._Int64, "long" },
        { InternalType._Float, "float" },
        { InternalType._Double, "double" },
        { InternalType._String, "std::string" },
        { InternalType._Char, "char" },
        { InternalType._Array, "std::vector<{0}>" },
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

    public string GetTypeOrThrow(InternalType type)
    {
        if (!_types.TryGetValue(type, out var value))
        {
            throw new Exception("Unsupported");
        }

        return value;
    }

    private string FormatArray(InternalType primitive, string value)
    {
        if (primitive is InternalType._String or InternalType._Char)
        {
            value = string.Join(",", value.Split(",").Select(v => ValueOfTypeAsString(primitive, v.Trim())));
        }
        return $"{{{value}}}";
    }
}
