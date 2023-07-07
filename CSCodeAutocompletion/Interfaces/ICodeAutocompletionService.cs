using CodeProblemAssistant.Contract.Models;
using CSTestContextBuilder.Core.Models;
using TestCases.Models;

namespace Cs.CodeAutocompletion.Interfaces
{
    public interface ICodeAutocompletionService
    {
        string GetNamespace(string namespaceName);
        string GetTestClass(CodeProblemMethodInfo methodInfo, string namespaceName);
        string GetTestCaseClass(List<TestCase> testCases, string namespaceName);
        string GetUsing(CSUsing csUsing);
        string GetUsing(List<CSUsing> usings);
        string GetUsing(List<string> usings);
        string GetUsing(string csUsing);
        string GetGlobalUsing(string csUsing);
        public string GetGlobalUsing(CSUsing csUsing);
        string GetGlobalUsing(List<string> csUsings);
        string GetGlobalUsing(List<CSUsing> csUsings);
        string GetSolutionTemplate(CodeProblemMethodInfo methodInfo);
        string GetParameters(List<CodeProblemParameterInfo> parameters);
    }
}