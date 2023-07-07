using Microsoft.CodeAnalysis.CSharp;
using Shared.Core.Compilers;
using Shared.Core.Extensions;
using SolutionValidator.Cs.Exceptions;
using SolutionValidator.Cs.Interfaces;
using SolutionValidator.Cs.Mappers;
using SolutionValidator.Cs.Models;
using SolutionValidator.Cs.Providers;
using SolutionValidator.Cs.Wrappers;
using System.Runtime.Loader;

namespace SolutionValidator.Cs.Services
{
    internal class CsCompilerAdapter : ICsCompilerAdapter
    {
        private readonly IExecutableReferenceService _referenceService;
        private readonly NetReferencesProvider _netReferencesProvider;
        private readonly ICompilationResultValidator _compilationResultValidator;
        private readonly IDiagnosticMapper _diagnosticMapper;
        private readonly AssemblyWrapper _assemblyWrapper;

        public CsCompilerAdapter(
            IExecutableReferenceService referenceService,
            NetReferencesProvider netReferencesProvider,
            ICompilationResultValidator compilationResultValidator,
            AssemblyWrapper assemblyWrapper,
            IDiagnosticMapper diagnosticMapper)
        {
            _referenceService = referenceService.ThrowIfNull();
            _netReferencesProvider = netReferencesProvider.ThrowIfNull();
            _compilationResultValidator = compilationResultValidator.ThrowIfNull();
            _diagnosticMapper = diagnosticMapper.ThrowIfNull();
            _assemblyWrapper = assemblyWrapper.ThrowIfNull();
        }

        public CompilationResult Compile(CompilationRequest request)
        {
            var references = _referenceService.GetReferenceFromLocation(request.Compilation.ReferenceLocations.ToArray()).ToList();
            references.AddRange(_netReferencesProvider.References);

            var compilation = CSharpCompilation.Create(
                GetAssemblyFileName(request.AssemblyName),
                request.Compilation.SyntaxTrees,
                references: references);

            var fileStream = new FileStream(request.AssemblyName, FileMode.Create, FileAccess.ReadWrite);

            var result = compilation.Emit(fileStream);

            try
            {
                _compilationResultValidator.Validate(result);
            }
            catch (CSCompilationException ex)
            {
                fileStream.Dispose();

                return new CompilationResult(_diagnosticMapper.Map(ex.Diagnostics));
            }

            return new CompilationResult(_assemblyWrapper.Wrap(new AssemblyLoadContext(request.AssemblyName, true), fileStream)); ;
        }

        private string GetAssemblyFileName(string assemblyName) => $"{assemblyName}.dll";
    }
}
