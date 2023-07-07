using Microsoft.Extensions.Logging;
using Shared.Core.Compilers;
using Shared.Core.Exceptions;
using Shared.Core.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Loader;
using System.Text;
using System.Threading.Tasks;

namespace CSCompiler.Core.Wrappers
{
    internal class CsInternalAssembly : IInternalAssembly
    {
        public Assembly? Assembly { get; private set; }

        private bool _disposed;
        private AssemblyLoadContext? _context;
        private FileStream? _fileStream;
        private readonly ILogger<CsInternalAssembly> _logger;

        public CsInternalAssembly(ILogger<CsInternalAssembly> logger)
        {
            _logger = logger.ThrowIfNull();
        }

        public int Execute()
        {
            if (Assembly == null)
            {
                throw new InvalidStateException("Assembly == null", nameof(Execute));
            }

            var entryPoint = Assembly.EntryPoint;

            if (entryPoint == null)
            {
                throw new InvalidOperationException($"Unable to execute assebmly without entry point.");
            }

            try
            {
                var result = entryPoint.GetParameters().Length > 0 ?
                    entryPoint.Invoke(null, new object[] { Array.Empty<string>() }) :
                    entryPoint.Invoke(null, null);

                return (result != null) ? (int)result : 0;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexpected error occurred while executing assembly {assemblyName}", Assembly.GetName());

                return -1;
            }
        }

        public void Dispose()
        {
            Dispose(true);

            GC.SuppressFinalize(this);
        }

        private protected virtual void Dispose(bool disposing)
        {
            if (_disposed)
            {
                return;
            }

            if (disposing)
            {
                UnloadAssembly();
            }

            _disposed = true;
        }

        private void UnloadAssembly()
        {
            _context?.Unload();
            if (_fileStream == null)
            {
                return;
            }
            if (_fileStream.CanRead)
            {
                _fileStream.Close();
            }
            File.Delete(_fileStream.Name);
            _fileStream?.Dispose();
        }

        public void LoadAssembly(AssemblyLoadContext context, FileStream fileStream)
        {
            _context = context;
            _fileStream = fileStream;
            if (_context == null || _fileStream == null)
            {
                throw new InvalidOperationException("Assebmly load context should be loaded before assembly.");
            }
            _fileStream.Seek(0, SeekOrigin.Begin);
            Assembly = _context.LoadFromStream(_fileStream);
            _fileStream.Close();
        }
    }
}
