using Shared.Core.Enums;

namespace InternalTypes.Interfaces
{
    public interface ITypeSetFactory
    {
        ITypeSet Get(CodeLanguage codeLanguage);
    }
}