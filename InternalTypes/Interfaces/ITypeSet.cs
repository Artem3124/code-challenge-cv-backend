using CodeProblemAssistant.Data.Enums;
using Shared.Core.Enums;

namespace InternalTypes.Interfaces
{
    public interface ITypeSet
    {
        CodeLanguage Language { get; init; }

        string TypeAsString(InternalType type);

        string ValueOfTypeAsString(InternalType type, string value);
    }
}