using CSCompiler.Contract.Interfaces;
using CSCompiler.Contract.Models;
using CSCompiler.Core.Interfaces;
using CSCompiler.Core.Models;
using Shared.Core.Compilers;
using Shared.Core.Extensions;

namespace CSCompiler.Core.Clients
{
    internal class CSCompilerClient : ICSCompilerClient
    {
        private readonly ICSCompiler _csCompiler;

        public CSCompilerClient(ICSCompiler csCompiler)
        {
            _csCompiler = csCompiler.ThrowIfNull();
        }

        public CompilationResult Compile(CSCompilationRequest request)
        {
            var result = _csCompiler.Compile(new CompilationRequest(request));

            return result;
        }
    }
}
