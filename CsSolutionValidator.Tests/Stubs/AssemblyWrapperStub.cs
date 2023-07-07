using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Moq;
using Shared.Core.Compilers;
using SolutionValidator.Cs.Wrappers;
using System.Runtime.Loader;

namespace SolutionValidator.Cs.Tests.Stubs
{
    internal class AssemblyWrapperStub : AssemblyWrapper
    {
        public AssemblyWrapperStub(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }

        public override IInternalAssembly Wrap(AssemblyLoadContext assemblyLoadContext, FileStream fileStream)
        {
            var serviceProvider = new ServiceCollection()
                                    .AddLogging()
                                    .BuildServiceProvider();

            var factory = serviceProvider.GetService<ILoggerFactory>();

            var logger = factory.CreateLogger<CsInternalAssembly>();

            return new CsInternalAssembly(logger);
        }
    }
}
