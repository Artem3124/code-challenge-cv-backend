using InternalTypes.Interfaces;
using Shared.Core.Enums;
using Shared.Core.Exceptions;

namespace InternalTypes.Types
{
    public class TypeSetFactory : ITypeSetFactory
    {
        public ITypeSet Get(CodeLanguage codeLanguage) => codeLanguage switch
        {
            CodeLanguage.Cpp => new CppTypeSet(),
            CodeLanguage.Cs => new CsTypeSet(),
            CodeLanguage.Python => new PythonTypeSet(),
            _ => throw new CodeLanguageNotSupportedException(codeLanguage.ToString()),
        };
    }
}
