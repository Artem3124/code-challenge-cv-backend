using CodeProblemAssistant.Contract.Models;
using CodeRunManager.Contract.Models;
using Cs.CodeAutocompletion.Interfaces;
using Cs.TestContextBuilder.Enums;
using Cs.TestContextBuilder.Interfaces;
using Cs.TestContextBuilder.Services;
using CSTestContextBuilder.Core.Models;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Shared.Core.Extensions;
using TestCases.Models;

namespace Cs.TestContextBuilder
{
    internal class CsTestContextBuilder : ICSContextBuilder
    {
        private readonly NUnitUsingsProvider _nUnitUsings;
        private readonly IUsingService _usingService;
        private readonly List<CSContextBuildOptions> _defaultBuildOpitons = new()
        {
            CSContextBuildOptions.AddNUnit,
            CSContextBuildOptions.AddEntryPoint,
            CSContextBuildOptions.AddTestCases,
        };
        private readonly ICodeAutocompletionService _autocompletionService;
        private readonly string _defaultNamespace = "Playground";
        public CsTestContextBuilder(IUsingService usingService, ICodeAutocompletionService autocompletionService, NUnitUsingsProvider nUnitUsings)
        {
            _nUnitUsings = nUnitUsings.ThrowIfNull();
            _usingService = usingService.ThrowIfNull();
            _autocompletionService = autocompletionService.ThrowIfNull();
        }

        public CSCompilationContext Build(string sourceCode, CodeProblemMethodInfo methodInfo, List<TestCase>? testCases = default, List<CSContextBuildOptions>? contextBuildOptions = default)
        {
            contextBuildOptions ??= _defaultBuildOpitons;
            var syntaxTrees = new List<SyntaxTree>
            {
                CSharpSyntaxTree.ParseText(sourceCode),
            };

            if (contextBuildOptions.Contains(CSContextBuildOptions.AddTestCases) && testCases != null)
            {
                AddSyntaxThree(syntaxTrees, () => _autocompletionService.GetTestCaseClass(testCases, _defaultNamespace));
                AddSyntaxThree(syntaxTrees, () => _autocompletionService.GetTestClass(methodInfo, _defaultNamespace));
            }
            var context = new CSCompilationContext(syntaxTrees);

            _usingService.AddUsingAndReference(context, _nUnitUsings.Get());

            return context;
        }

        private void AddSyntaxThree(List<SyntaxTree> syntaxTrees, Func<string> func) => syntaxTrees.Add(CSharpSyntaxTree.ParseText(func()));
    }
}
