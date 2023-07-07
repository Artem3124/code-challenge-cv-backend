using CppCodeAutocomplition;
using CppTestContextBuilder.Core.Models;
using CppTestContextBuilder.Interfaces;
using Shared.Core.Extensions;

namespace CppTestContextBuilder.Services
{
    internal class IncludeService : IIncludeService
    {
        private readonly CppCodeAutocomplitionService _codeAutocomplitionService;

        public IncludeService(CppCodeAutocomplitionService codeAutocomplitionService)
        {
            _codeAutocomplitionService = codeAutocomplitionService.ThrowIfNull();
        }

        public CppCompilationContext AddIncludes(CppCompilationContext context, string fileName, List<Include> includes)
        {
            if (!includes.Any())
            {
                return context;
            }

            var headerFile = new HeaderFile(
                fileName,
                _codeAutocomplitionService.FormatHeaderFile(fileName, _codeAutocomplitionService.GetInclude(includes))
            );

            context.Files.ForEach(f => f.Content = $"{_codeAutocomplitionService.GetInternalInclude(headerFile.Name)}\n{f.Content}");
            context.EntryPointFile.Content = $"{_codeAutocomplitionService.GetInternalInclude(headerFile.Name)}\n{context.EntryPointFile.Content}";
            context.Files.Add(headerFile);

            includes.ForEach(i =>
            {
                if (!string.IsNullOrWhiteSpace(i.ReferenceLocation))
                {
                    context.References.Add(i.ReferenceLocation);
                }
            });

            return context;
        }
    }
}
