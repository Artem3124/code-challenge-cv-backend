using Microsoft.CodeAnalysis.CSharp;
using Shared.Core.Extensions;
using SolutionValidator.Cs.Interfaces;
using SolutionValidator.Cs.Models;

namespace SolutionValidator.Cs.Services
{
    internal class UsingService : IUsingService
    {
        private readonly ICodeAutocompletionService _autocompletionService;

        public UsingService(ICodeAutocompletionService autocompletionService)
        {
            _autocompletionService = autocompletionService.ThrowIfNull();
        }

        public CsCompilationContext AddUsingAndReference(CsCompilationContext compilation, CSUsing csUsing)
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

        public CsCompilationContext AddUsingAndReference(CsCompilationContext compilation, List<CSUsing> csUsings)
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
