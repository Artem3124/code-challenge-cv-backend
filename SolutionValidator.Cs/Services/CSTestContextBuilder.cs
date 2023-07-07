using CodeProblemAssistant.Contract.Models;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Shared.Core.Extensions;
using SolutionValidator.Cs.Enums;
using SolutionValidator.Cs.Interfaces;
using SolutionValidator.Cs.Models;
using SolutionValidator.Cs.Providers;
using TestCases.Models;

namespace SolutionValidator.Cs.Services
{
    internal class CsTestContextBuilder : ICsContextBuilder
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

        public CsCompilationContext Build(string sourceCode, CodeProblemMethodInfo methodInfo, List<TestCase>? testCases = default, List<CSContextBuildOptions>? contextBuildOptions = default)
        {
            contextBuildOptions ??= _defaultBuildOpitons;
            var syntaxTrees = new List<SyntaxTree>
            {
                CSharpSyntaxTree.ParseText(sourceCode),
            };

            if (contextBuildOptions.Contains(CSContextBuildOptions.AddTestCases) && testCases?.Any() == true)
            {
                AddSyntaxThree(syntaxTrees, () => _autocompletionService.GetTestCaseClass(testCases, _defaultNamespace));
                AddSyntaxThree(syntaxTrees, () => _autocompletionService.GetTestClass(methodInfo, _defaultNamespace));
            }
            else
            {
                AddSyntaxThree(syntaxTrees, () => _autocompletionService.GetEntryPoint());
            }
            var context = new CsCompilationContext(syntaxTrees);

            _usingService.AddUsingAndReference(context, _nUnitUsings.Get());

            return context;
        }

        private void AddSyntaxThree(List<SyntaxTree> syntaxTrees, Func<string> func) => syntaxTrees.Add(CSharpSyntaxTree.ParseText(func()));
    }
}
