using CSCompiler.Contract.Models;
using CSCompiler.Core.Exceptions;
using CSCompiler.Core.Interfaces;
using CSCompiler.Core.Mappers;
using CSCompiler.Core.Models;
using CSCompiler.Core.Providers;
using CSCompiler.Core.Wrappers;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.Extensions.Logging;
using Shared.Core.Compilers;
using Shared.Core.Extensions;
using System.Runtime.Loader;

namespace CSCompiler.Core.Services
{
    internal class CSCompiler : ICSCompiler
    {
        private readonly IExecutableReferenceService _referenceService;
        private readonly NetReferencesProvider _netReferencesProvider;
        private readonly ILogger<CSCompiler> _logger;
        private readonly ICompilationResultValidator _compilationResultValidator;
        private readonly IDiagnosticMapper _diagnosticMapper;
        private readonly AssemblyWrapper _assemblyWrapper;

        public CSCompiler(
            IExecutableReferenceService referenceService,
            NetReferencesProvider netReferencesProvider,
            ILogger<CSCompiler> logger,
            ICompilationResultValidator compilationResultValidator,
            AssemblyWrapper assemblyWrapper,
            IDiagnosticMapper diagnosticMapper)
        {
            _referenceService = referenceService.ThrowIfNull();
            _netReferencesProvider = netReferencesProvider.ThrowIfNull();
            _logger = logger.ThrowIfNull();
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
