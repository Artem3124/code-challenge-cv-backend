using Shared.Core.Enums;

namespace Gateway.Contact.Models
{

    public class CodeProblemTemplate
    {
        public CodeLanguage CodeLanguage { get; set; }

        public string Template { get; set; }

        public CodeProblemTemplate(CodeLanguage codeLanguage, string template)
        {
            CodeLanguage = codeLanguage;
            Template = template;
        }
    }
}
