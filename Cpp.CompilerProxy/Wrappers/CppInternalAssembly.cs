using FileScopeProvider.Interfaces;
using Shared.Core.Compilers;
using Shared.Core.Extensions;
using Shared.Core.Interfaces;
using System.Diagnostics;

namespace Cpp.CompilerProxy.Core.Wrappers
{
    public class CppInternalAssembly : IInternalAssembly
    {
        private readonly ISystemProcessService _systemProcessService;
        private readonly IFilePurger _fileDisposer;
        private bool _disposed;

        public string? ExecutableName { get; set; }

        public CppInternalAssembly(ISystemProcessService systemProcessService, IFilePurger fileDisposer)
        {
            _systemProcessService = systemProcessService.ThrowIfNull();
            _fileDisposer = fileDisposer.ThrowIfNull();
        }

        public void Dispose()
        {
            if (_disposed || string.IsNullOrWhiteSpace(ExecutableName))
            {
                return;
            }

            _fileDisposer.Delete($"{ExecutableName}.exe");
            _disposed = true;
            GC.SuppressFinalize(this);
        }

        public int Execute()
        {
            if (ExecutableName == default)
            {
                throw new Exception("Executable name was null.");
            }

            return _systemProcessService.Execute(new ProcessStartInfo
            {
                FileName = ExecutableName,
            }).ExitCode;
        }

        public void Load(string executableName)
        {
            ExecutableName = executableName;
        }
    }
}
