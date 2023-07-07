using InternalTypes.Interfaces;
using Shared.Core.Enums;
using Shared.Core.Extensions;
using TestCases.Models;
using InternalType = CodeProblemAssistant.Data.Enums.InternalType;

namespace InternalTypes.Converters
{
    public class TestCaseConverter : ITestCaseConverter
    {
        private readonly ITypeConverter _typeConverter;

        public TestCaseConverter(ITypeConverter typeConverter)
        {
            _typeConverter = typeConverter.ThrowIfNull();
        }

        public List<TestCase> Convert(CodeLanguage codeLanguage, List<InternalType> parameterTypes, InternalType returnType, List<TestCase> testCases)
        {
            return testCases.Select(t => Convert(codeLanguage, parameterTypes, returnType, t)).ToList();
        }

        public TestCase Convert(CodeLanguage codeLanguage, List<InternalType> parameterTypes, InternalType returnType, TestCase testCase)
        {
            testCase.Expected = Convert(codeLanguage, returnType, testCase.Expected);
            for (int i = 0; i < testCase.Input.Count; i++)
            {
                testCase.Input[i] = Convert(codeLanguage, parameterTypes[i], testCase.Input[i]);
            }
            return testCase;
        }

        private string Convert(CodeLanguage codeLanguage, InternalType type, string value)
        {
            return _typeConverter.ValueOfTypeAsString(codeLanguage, type, value);
        }
    }
}
