using CodeProblemAssistant.Contract.Models;
using TestCases.Models;

namespace Python.CodeAutocomplition
{
    public class PythonCodeAutocomplitionService
    {
        public string WrapForSyntaxCheck(TestCase testCase, CodeProblemMethodInfo methodInfo)
        {
            return $"{CodeTemplates.Main}{methodInfo.Name}({string.Join(", ", testCase.Input)})";
        }

        public string WrapForTesting(List<TestCase> testCases, CodeProblemMethodInfo methodInfo, string sourceCodeFileName)
        {
            var testCasesString = string.Join('\n', testCases.Select(t => $"\t{string.Format(CodeTemplates.TestCaseDefinition, t.Id)}\t\t" +
                $"{string.Format(CodeTemplates.TestCaseContent, sourceCodeFileName, methodInfo.Name, string.Join(", ", t.Input), t.Expected)}"));

            var imports = $"{CodeTemplates.Import.Replace("{importName}", "unittest")}\n" +
                $"{CodeTemplates.Import.Replace("{importName}", sourceCodeFileName)}\n";

            var testMethodString = $"{imports}{CodeTemplates.TestMethodDefinition}{testCasesString}\n";

            return testMethodString + CodeTemplates.Main + CodeTemplates.UnitTestMainContent;
        }

        public string GetSolutionTemplate(CodeProblemMethodInfo methodInfo)
        {
            return CodeTemplates.SolutionTemplate
                .Replace("{methodName}", methodInfo.Name)
                .Replace("{inpust}", string.Join(", ", methodInfo.Parameters.Select(p => p.Name)));
        }
    }
}
