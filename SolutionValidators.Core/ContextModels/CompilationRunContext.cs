using Shared.Core.Compilers;
using Shared.Core.Enums;
using Shared.Core.Extensions;
using SolutionValidators.Core.Exceptions;

namespace SolutionValidators.Core.ContextModels
{
    public class CompilationRunContext : RunContextBase
    {
        public CompilationRunContext(IRunContext context) : base(context)
        {

        }

        public override void Validate()
        {
            if (Diagnostics.Any(d => d.Severity == DiagnosticSeverityInternal.Error || d.WarningAsError))
            {
                var metadata = new Dictionary<string, string>();
                metadata.AddMetadataEntry(Diagnostics, MetadataEntryNames.CompilationErrors);

                throw new CodeRunException(CodeRunOutcome.CompilationError, metadata);
            }
        }
    }
}
