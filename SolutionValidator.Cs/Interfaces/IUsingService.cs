using SolutionValidator.Cs.Models;

namespace SolutionValidator.Cs.Interfaces
{
    internal interface IUsingService
    {
        CsCompilationContext AddUsingAndReference(CsCompilationContext compilation, CSUsing csUsing);
        CsCompilationContext AddUsingAndReference(CsCompilationContext compilation, List<CSUsing> csUsing);
    }
}