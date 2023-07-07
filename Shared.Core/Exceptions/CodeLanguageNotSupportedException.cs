using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Core.Exceptions
{
    public class CodeLanguageNotSupportedException : Exception
    {
        public CodeLanguageNotSupportedException(string codeLanguage) : base($"Code language {codeLanguage} is not supported.")
        {

        }
    }
}
