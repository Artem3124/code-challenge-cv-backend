using Cpp.CompilerProxy.Exceptions;
using Cpp.CompilerProxy.Models;
using Cpp.CompilerProxy.Services;
using Cpp.CompilerProxy.Wrappers;
using FileScopeProvider.Interfaces;
using FileScopeProvider.Models;
using Shared.Core.Compilers;
using Shared.Core.Extensions;
using Shared.Core.Interfaces;
using System.Diagnostics;

namespace Cpp.CompilerProxy
{
    internal class CppCompilerAdapter : ICppCompilerAdapter
    {
        private readonly IFileNameGenerator _fileNameGenerator;
        private readonly ISystemProcessService _systemProcessService;
        private readonly IFilePurger _filePurger;
        private readonly IAssemblyWrapper _assemblyWrapper;
        private readonly ICompilationDiagnosticCheckService _diagnosticCheckService;
        private readonly IDiagnosticParser _diagnosticParser;
        private readonly IFileScopeProvider _fileScopeProvider;

        public CppCompilerAdapter(
            IFileNameGenerator fileNameGenerator,
            ISystemProcessService systemProcessService,
            IFilePurger filePurger,
            IAssemblyWrapper assemblyWrapper,
            ICompilationDiagnosticCheckService diagnosticCheckService,
            IDiagnosticParser diagnosticParser,
            IFileScopeProvider fileScopeProvider)
        {
            _fileNameGenerator = fileNameGenerator.ThrowIfNull();
            _systemProcessService = systemProcessService.ThrowIfNull();
            _filePurger = filePurger.ThrowIfNull();
            _assemblyWrapper = assemblyWrapper.ThrowIfNull();
            _diagnosticCheckService = diagnosticCheckService.ThrowIfNull();
            _diagnosticParser = diagnosticParser.ThrowIfNull();
            _fileScopeProvider = fileScopeProvider.ThrowIfNull();
        }

        public CompilationResult Compile(CppCompilationRequest request)
        {
            var resultFileName = _fileNameGenerator.Generate();
            var entryPoint = request.Context.EntryPointFile;
            var entryPointFileName = GetFileName(entryPoint.Name);

            using var fileScope = _fileScopeProvider.Create(new List<SystemFile>(request.Context.Files)
            {
                entryPoint,
            });
            try
            {
                var processStartInfo = new ProcessStartInfo
                {
                    UseShellExecute = false,
                    RedirectStandardError = true,
                    CreateNoWindow = true,
                    FileName = "C:/mingw32/bin/c++",
                    Arguments = GetArguments(entryPointFileName, request.Context.References),
                };
                var output = _systemProcessService.Execute(processStartInfo);

                var diagnostic = !string.IsNullOrEmpty(output.StandardError)
                    ? _diagnosticParser.StringToDiagnostic(output.StandardError)
                    : new();
                _diagnosticCheckService.MarkWarningsAsErrors(diagnostic);

                if (output.ExitCode != 0 && diagnostic.Count == 0)
                {
                    throw new CppCompilerProxyInternalException(output.ExitCode);
                }

                var assembly = default(IInternalAssembly?);
                var hasError = diagnostic.Any(d => d.Severity == DiagnosticSeverityInternal.Error || d.WarningAsError);
                if (hasError && output.ExitCode == 0)
                {
                    _filePurger.Delete($"{entryPointFileName}.exe");
                }
                else if (!hasError)
                {
                    assembly = _assemblyWrapper.Wrap(entryPointFileName);
                }

                return new CompilationResult(assembly, diagnostic);
            }
            finally
            {
                _filePurger.Delete(resultFileName);
            }
        }

        private string GetFileName(string value) => string.Join('.', value.Split(".").SkipLast(1));

        private string GetArguments(string fileName, IEnumerable<string> linkAssemblies) =>
            $"-o {fileName}.exe {string.Join(' ', linkAssemblies)} {fileName}.cpp";
    }
}
