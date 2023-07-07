using Cs.CodeAutocompletion.Interfaces;
using Cs.TestContextBuilder.Interfaces;
using CSTestContextBuilder.Core.Models;
using Microsoft.CodeAnalysis.CSharp;
using Shared.Core.Extensions;

namespace Cs.TestContextBuilder.Services
{
    internal class UsingService : IUsingService
    {
        private readonly ICodeAutocompletionService _autocompletionService;

        public UsingService(ICodeAutocompletionService autocompletionService)
        {
            _autocompletionService = autocompletionService.ThrowIfNull();
        }

        public CSCompilationContext AddUsingAndReference(CSCompilationContext compilation, CSUsing csUsing)
        {
            compilation.ThrowIfNull();
            csUsing.ThrowIfNull();

            compilation.SyntaxTrees.Add(CSharpSyntaxTree.ParseText(_autocompletionService.GetGlobalUsing(csUsing)));

            if (!string.IsNullOrWhiteSpace(csUsing.ReferenceLocation))
            {
                compilation.ReferenceLocations.Add(csUsing.ReferenceLocation);
            }

            return compilation;
        }

        public CSCompilationContext AddUsingAndReference(CSCompilationContext compilation, List<CSUsing> csUsings)
        {
            compilation.ThrowIfNull();
            csUsings.ThrowIfNull();

            if (csUsings.Count == 0)
            {
                return compilation;
            }
            compilation.SyntaxTrees.Add(CSharpSyntaxTree.ParseText(_autocompletionService.GetGlobalUsing(csUsings)));
            csUsings.ForEach(u =>
            {
                if (!string.IsNullOrWhiteSpace(u.ReferenceLocation))
                {
                    compilation.ReferenceLocations.Add(u.ReferenceLocation);
                }
            });

            return compilation;
        }
    }
}
