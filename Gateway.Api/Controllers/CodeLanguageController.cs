using Gateway.Api.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Shared.Core.Enums;
using Shared.Core.Extensions;

namespace Gateway.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CodeLanguageController : ControllerBase
    {
        private readonly ICodeLanguageTypeValidator _codeLanguageTypeValidator;

        public CodeLanguageController(ICodeLanguageTypeValidator codeLanguageTypeValidator)
        {
            _codeLanguageTypeValidator = codeLanguageTypeValidator.ThrowIfNull();
        }

        [HttpGet]
        public ActionResult<List<CodeLanguage>> GetSupportedCodeLanguages()
        {
            return Ok(_codeLanguageTypeValidator.GetSupportedCodeLanguages());
        }
    }
}
