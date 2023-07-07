using CodeProblemAssistant.Data.Enums;
using Shared.Core.Enums;
using TestCases.Models;

namespace InternalTypes.Interfaces
{
    public interface ITestCaseConverter
    {
        List<TestCase> Convert(CodeLanguage codeLanguage, List<InternalType> parameterTypes, InternalType returnType, List<TestCase> testCases);
        TestCase Convert(CodeLanguage codeLanguage, List<InternalType> parameterTypes, InternalType returnType, TestCase testCase);
    }
}