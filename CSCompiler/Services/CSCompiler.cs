using CSCompiler.Interfaces;
using CSCompiler.Models;
using CSCompiler.Providers;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using NoName.Core.Interfaces;
using System.Reflection;
using System.Runtime.Loader;

namespace CSCompiler.Services
{
    internal class CSCompiler : ICSCompiler
    {
        private readonly IExecutableReferenceService _referenceService;
        private readonly NetReferencesProvider _netReferencesProvider;
        private readonly ILogger _logger;

        public CSCompiler(
            IExecutableReferenceService referenceService,
            NetReferencesProvider netReferencesProvider,
            ILogger logger)
        {
            _referenceService = referenceService ?? throw new ArgumentNullException(nameof(referenceService));
            _netReferencesProvider = netReferencesProvider ?? throw new ArgumentNullException(nameof(netReferencesProvider));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public Assembly Compile(CSCompilationRequest request)
        {
            var references = _referenceService.GetReferenceFromLocation(request.Compilation.ReferenceLocations.ToArray()).ToList();
            references.AddRange(_netReferencesProvider.References);

            var compilation = CSharpCompilation.Create(
                GetAssemblyFileName(request.AssemblyName),
                GetSyntaxTree(request.Compilation.SourceCode),
                references: references);

            using var fileStream = new FileStream(request.AssemblyName, FileMode.Create, FileAccess.ReadWrite);

            var result = compilation.Emit(fileStream);
            fileStream.Seek(0, SeekOrigin.Begin);

            return AssemblyLoadContext.Default.LoadFromStream(fileStream);
        }

        private string GetAssemblyFileName(string assemblyName) => $"{assemblyName}.dll";

        private SyntaxTree[] GetSyntaxTree(string sourceCode) => new[] { CSharpSyntaxTree.ParseText(sourceCode) };
    }
}
