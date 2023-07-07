using CSCompiler.Extensions;
using CSCompiler.Interfaces;
using CSCompiler.Models;

namespace CSCompiler.Providers
{
    internal class NUnitUsingsProvider : IUsingsProvider
    {
        public List<CSUsing> Get() => new()
        {
            new ("NUnit.Framework", typeof(NUnit.Framework.Assert).GetAssemblyLocation()),
            new ("NUnitLite", typeof(NUnitLite.AutoRun).GetAssemblyLocation()),
        };
    }
}
