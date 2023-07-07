using CodeProblemAssistant.Data.Enums;
using InternalTypes.Interfaces;
using Shared.Core.Enums;

namespace InternalTypes.Types;

internal abstract class TypeSetBase : ITypeSet
{
    protected readonly Dictionary<InternalType, string> _types;

    public TypeSetBase(CodeLanguage language, Dictionary<InternalType, string> types)
    {
        Language = language;
        _types = types;
    }

    public CodeLanguage Language { get; init; }

    public abstract string TypeAsString(InternalType type);

    public abstract string ValueOfTypeAsString(InternalType type, string value);

    protected bool IsArray(InternalType type) => type >= InternalType._Array;

    protected InternalType GetTypeOfArray(InternalType type)
    {
        if (!IsArray(type))
        {
            return type;
        }

        return (InternalType)Enum.Parse(typeof(InternalType), (type - InternalType._Array).ToString());
    }
}