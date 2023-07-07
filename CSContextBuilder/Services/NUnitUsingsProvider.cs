using Cs.TestContextBuilder.Interfaces;
using CSTestContextBuilder.Core.Models;
using NUnit.Framework;
using NUnit.Framework.Internal;
using NUnitLite;
using System.Reflection;

namespace Cs.TestContextBuilder.Services
{
    internal class NUnitUsingsProvider
    {
        public List<CSUsing> Get() => new()
        {
            new CSUsing("NUnit.Framework.Internal", GetAssemblyLocationFromType(typeof(Test))),
            new CSUsing("NUnitLite", GetAssemblyLocationFromType(typeof(AutoRun))),
            new CSUsing("NUnit.Framework", GetAssemblyLocationFromType(typeof(TestCaseSourceAttribute))),
            new CSUsing("System.Collections"),
        };

        private string GetAssemblyLocationFromType(Type type) => type.GetTypeInfo().Assembly.Location;
    }
}
