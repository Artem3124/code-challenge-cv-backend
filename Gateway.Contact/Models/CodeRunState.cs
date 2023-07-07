using CodeRunManager.Contract.Models;
using Shared.Core.Enums;

namespace Gateway.Contact.Models
{
    public class CodeRunProgress
    {
        public CodeRunStage Stage { get; set; }

        public CodeRunResultExpanded? Result { get; set; }
    }
}
