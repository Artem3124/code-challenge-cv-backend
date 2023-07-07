using Mongo.Data.Models;
using Shared.Core.Extensions;

namespace SolutionValidators.Core.ContextModels
{
    public class CodeRunContext : RunContextBase
    {
        public CodeRunContext(IRunContext context) : base(context)
        {

        }

        public CodeRunContext(CodeRunQueueMessage codeRun) : base(codeRun)
        {

        }

        public override void Validate()
        {
            CodeRun.ThrowIfNull();
            if (CodeProblem is null && Challenge is null)
            {
                throw new ArgumentNullException(nameof(CodeProblem));
            }
            TestCases.ThrowIfNull();
        }
    }
}
