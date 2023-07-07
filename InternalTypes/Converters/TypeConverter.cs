using InternalTypes.Interfaces;
using Shared.Core.Enums;
using Shared.Core.Extensions;
using InternalType = CodeProblemAssistant.Data.Enums.InternalType;

namespace InternalTypes.Converters
{
    public class TypeConverter : ITypeConverter
    {
        private readonly ITypeSetFactory _testSetFactory;

        public TypeConverter(ITypeSetFactory testSetFactory)
        {
            _testSetFactory = testSetFactory.ThrowIfNull();
        }

        public string TypeAsString(CodeLanguage codeLanguage, InternalType type)
        {
            var testSet = _testSetFactory.Get(codeLanguage);

            return testSet.TypeAsString(type);
        }

        public string ValueOfTypeAsString(CodeLanguage codeLanguage, InternalType type, string value)
        {
            var testSet = GetTypeSet(codeLanguage);

            return testSet.ValueOfTypeAsString(type, value);
        }

        private ITypeSet GetTypeSet(CodeLanguage codeLanguage) => _testSetFactory.Get(codeLanguage);
    }
}
