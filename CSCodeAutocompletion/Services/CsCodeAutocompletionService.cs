using CodeProblemAssistant.Contract.Models;
using CodeProblemAssistant.Data.Enums;
using Cs.CodeAutocompletion.Interfaces;
using Cs.CodeAutocompletion.Providers;
using CSTestContextBuilder.Core.Models;
using InternalTypes.Interfaces;
using Shared.Core.Enums;
using Shared.Core.Extensions;
using System.Runtime.CompilerServices;
using TestCases.Models;

namespace Cs.CodeAutocompletion.Services
{
    public class CsCodeAutocompletionService : ICodeAutocompletionService
    {
        private readonly ITypeSet _typeSet;

        public CsCodeAutocompletionService(ITypeSetFactory typeSetFactory)
        {
            if (typeSetFactory is null)
            {
                throw new ArgumentNullException(nameof(typeSetFactory));
            }

            _typeSet = typeSetFactory.Get(CodeLanguage.Cs).ThrowIfNull();
        }

        public string GetTestCaseClass(List<TestCase> testCases, string namespaceName)
        {
            var testCasesSourceCode = string.Join(
                string.Empty,
                testCases.Select(t => string.Format(CodeTemplates.NUnitTestCase, string.Join(", ", t.Input), t.Expected))
            );
            namespaceName = GetNamespace(namespaceName);

            return CodeTemplates.NUnitTestCaseFactory
                .Replace(testCasesSourceCode)
                .Replace(namespaceName);
        }

        public string GetTestClass(CodeProblemMethodInfo methodInfo, string namespaceName)
        {
            var returnType = GetReturnType(methodInfo.ReturnType);
            var methodName = TitleCase(methodInfo.Name);
            var parameters = methodInfo.Parameters;
            var args = GetParameters(parameters);
            var callArgs = string.Join(',', parameters.Select(p => p.Name));
            namespaceName = namespaceName = GetNamespace(namespaceName);

            return CodeTemplates.TestSubject
                .Replace(returnType)
                .Replace(methodName)
                .Replace(args)
                .Replace(callArgs)
                .Replace(namespaceName);
        }

        public string GetSolutionTemplate(CodeProblemMethodInfo methodInfo)
        {
            var returnType = GetReturnType(methodInfo.ReturnType);
            var methodName = TitleCase(methodInfo.Name);
            var args = GetParameters(methodInfo.Parameters);

            return CodeTemplates.SolutionTemplate
                .Replace(returnType)
                .Replace(methodName)
                .Replace(args);
        }

        public string GetNamespace(string namespaceName) => string.Format(CodeTemplates.Namespace, namespaceName);

        public string GetParameters(List<CodeProblemParameterInfo> parameters) =>
            string.Join(", ", parameters.Select(p => $"{_typeSet.TypeAsString(p.Type)} {p.Name}"));

        public string GetReturnType(InternalType value) => _typeSet.TypeAsString(value);

        public string GetUsing(CSUsing csUsing) => GetUsing(csUsing.Namespace);

        public string GetUsing(string csUsing) => string.Format(CodeTemplates.Using, csUsing);

        public string GetUsing(List<CSUsing> csUsings) => string.Join(string.Empty, csUsings.Select(u => GetUsing(u)));

        public string GetUsing(List<string> csUsings) => string.Join(string.Empty, csUsings.Select(u => GetUsing(u)));

        public string GetGlobalUsing(string csUsing) => string.Format(CodeTemplates.GlobalUsing, csUsing);

        public string GetGlobalUsing(CSUsing csUsing) => GetGlobalUsing(csUsing.Namespace);

        public string GetGlobalUsing(List<string> csUsings) => string.Join(string.Empty, csUsings.Select(u => GetGlobalUsing(u)));

        public string GetGlobalUsing(List<CSUsing> csUsings) => string.Join(string.Empty, csUsings.Select(u => GetGlobalUsing(u)));

        private string TitleCase(string value) => string.Concat(value[0].ToString().ToUpper(), value.AsSpan(1));
    }

    internal static class StringExtensions
    {
        public static string Replace(this string value, string newValue, [CallerArgumentExpression("newValue")] string? oldValue = default) =>
            value.Replace($"{{{oldValue}}}" ?? string.Empty, newValue);
    }
}