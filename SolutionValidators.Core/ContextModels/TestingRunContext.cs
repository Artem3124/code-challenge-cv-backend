using Shared.Core.Enums;
using Shared.Core.Extensions;
using SolutionValidators.Core.Exceptions;

namespace SolutionValidators.Core.ContextModels
{
    public class TestingRunContext : RunContextBase
    {
        public TestingRunContext(IRunContext context) : base(context)
        {

        }

        public override void Validate()
        {
            var failedTestCase = TestCaseResults?.FirstOrDefault(t => t.Result.Equals("failed", StringComparison.OrdinalIgnoreCase));
            if (failedTestCase != null)
            {
                var metadata = new Dictionary<string, string>();
                metadata.AddMetadataEntry(failedTestCase, MetadataEntryNames.FailedTestCase);

                throw new CodeRunException(CodeRunOutcome.TestFailed, metadata);
            }
        }
    }
}
