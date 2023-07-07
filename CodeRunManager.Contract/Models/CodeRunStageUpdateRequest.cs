using Shared.Core.Enums;

namespace CodeRunManager.Contract.Models
{
    public class CodeRunStageUpdateRequest
    {
        public CodeRunStage Stage { get; set; }

        public CodeRunStageUpdateRequest(CodeRunStage stage)
        {
            Stage = stage;
        }
    }
}
