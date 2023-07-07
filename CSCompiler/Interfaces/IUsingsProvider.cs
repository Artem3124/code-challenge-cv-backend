using CSCompiler.Models;

namespace CSCompiler.Interfaces
{
    internal interface IUsingsProvider
    {
        List<CSUsing> Get();
    }
}
