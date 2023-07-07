using CodeProblemAssistant.Data.Enums;
using Shared.Core.Enums;

namespace InternalTypes.Interfaces
{
    public interface ITypeConverter
    {
        string TypeAsString(CodeLanguage codeLanguage, InternalType type);
        string ValueOfTypeAsString(CodeLanguage codeLanguage, InternalType type, string value);
    }
}