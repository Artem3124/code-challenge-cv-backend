using Shared.Core.Compilers;
using Shared.Core.Extensions;
using SolutionValidator.Cs.Interfaces;
using SolutionValidator.Cs.Models;

namespace SolutionValidator.Cs.Clients
{
    internal class CsCompilerClient : ICsCompilerClient
    {
        private readonly ICsCompilerAdapter _csCompiler;

        public CsCompilerClient(ICsCompilerAdapter csCompiler)
        {
            _csCompiler = csCompiler.ThrowIfNull();
        }

        public CompilationResult Compile(CSCompilationRequest request)
        {
            return _csCompiler.Compile(new CompilationRequest(request)); ;
        }
    }
}
