using CodeProblemAssistant.Contract.Models;
using CppTestContextBuilder.Core.Models;
using InternalTypes.Interfaces;
using Shared.Core.Enums;
using Shared.Core.Extensions;
using System.Runtime.CompilerServices;
using TestCases.Models;

namespace CppCodeAutocomplition
{
    public class CppCodeAutocomplitionService
    {
        private readonly ITypeSet _typeSet;

        public CppCodeAutocomplitionService(ITypeSetFactory typeSetFactory)
        {
            typeSetFactory.ThrowIfNull();
            _typeSet = typeSetFactory.Get(CodeLanguage.Cpp);
        }

        public string WrapForTesting(string sourceCode, CodeProblemMethodInfo methodInfo, List<TestCase> testCases)
        {
            sourceCode = AddTestCaseClass(sourceCode, methodInfo);
            sourceCode = AddTestCaseFactory(sourceCode, methodInfo);

            return AddEntryPoint(sourceCode, testCases);
        }

        public string AddTestCaseClass(string sourceCode, CodeProblemMethodInfo methodInfo)
        {
            var expectedType = _typeSet.TypeAsString(methodInfo.ReturnType);
            var inputsEnumeration = methodInfo.Parameters.Select(p => $"{_typeSet.TypeAsString(p.Type)} {p.Name}");
            var inputs = string.Join(',', inputsEnumeration);
            var inputFields = string.Join("\n\r", inputsEnumeration.Select(i => $"{i};"));
            var inputInitializations = string.Join("\n\r", methodInfo.Parameters.Select(p => CodeTemplates.TestCaseInputInitialization.Replace("{fieldName}", p.Name)));
            var testCaseClass = CodeTemplates.TestCaseClass
                .Replace(expectedType)
                .Replace(inputs)
                .Replace(inputFields)
                .Replace(inputInitializations);

            return $"{sourceCode}\n{testCaseClass}";
        }

        public string AddTestCaseFactory(string sourceCode, CodeProblemMethodInfo methodInfo)
        {
            var expectedType = _typeSet.TypeAsString(methodInfo.ReturnType);
            var methodName = CamelCase(methodInfo.Name);
            var instanceName = "testCase";
            var testCaseInputs = string.Join(',', methodInfo.Parameters.Select(p => CodeTemplates.CallArg.Replace("{fieldName}", p.Name))).Replace(instanceName);

            var testCaseFactoryClass = CodeTemplates.TestCaseFactoryClass
                .Replace(expectedType)
                .Replace(methodName)
                .Replace(testCaseInputs);

            return $"{sourceCode}\n{testCaseFactoryClass}";
        }

        public string AddEntryPoint(string sourceCode, List<TestCase> testCases)
        {
            var testCasesInitialization = string.Join(",\n", testCases.Select(
                t => CodeTemplates.TestCaseInitialization
                    .Replace("{id}", t.Id.ToString())
                    .Replace("{expected}", t.Expected)
                    .Replace("{inputs}", string.Join(',', t.Input))
            ));

            var entryPoint = CodeTemplates.EntryPoint
                .Replace(testCasesInitialization);

            return $"{sourceCode}\n{entryPoint}";
        }

        public string GetSolutionTemplate(CodeProblemMethodInfo methodInfo)
        {
            var returnType = _typeSet.TypeAsString(methodInfo.ReturnType);
            var methodName = CamelCase(methodInfo.Name);
            var args = GetParameters(methodInfo.Parameters);

            return CodeTemplates.SolutionTemplate
                .Replace(returnType)
                .Replace(methodName)
                .Replace(args);
        }

        public string GetInternalInclude(string name) => string.Format(CodeTemplates.InternalInclude, name);

        public string GetExternalInclude(string name) => string.Format(CodeTemplates.ExteranlInclude, name);

        public string FormatHeaderFile(string fileName, string content)
        {
            return CodeTemplates.HeaderFileTemplate
                .Replace(fileName)
                .Replace(content);
        }

        public string GetInclude(Include include) => include.Type == IncludeType.Internal ? GetInternalInclude(include.Name) : GetExternalInclude(include.Name);

        public string GetInclude(List<Include> includes) => string.Join('\n', includes.Select(i => GetInclude(i)));

        private string GetParameters(List<CodeProblemParameterInfo> parameterInfos)
        {
            return string.Join(',', parameterInfos.Select(p => $"{_typeSet.TypeAsString(p.Type)} {p.Name}"));
        }

        private string CamelCase(string value) => string.Concat(value[0].ToString().ToLower(), value.AsSpan(1));

    }

    internal static class StringExtensions
    {
        public static string Replace(this string value, string newValue, [CallerArgumentExpression("newValue")] string? oldValue = default) =>
            value.Replace($"{{{oldValue}}}" ?? string.Empty, newValue);
    }
}
