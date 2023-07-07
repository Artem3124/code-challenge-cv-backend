using CSTestContextBuilder.Core.Models;

namespace Cs.TestContextBuilder.Interfaces
{
    internal interface IUsingService
    {
        CSCompilationContext AddUsingAndReference(CSCompilationContext compilation, CSUsing csUsing);
        CSCompilationContext AddUsingAndReference(CSCompilationContext compilation, List<CSUsing> csUsing);
    }
}