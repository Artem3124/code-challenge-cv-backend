﻿using Shared.Core.Compilers;

namespace Cpp.CompilerProxy.Services
{
    internal class CompilationDiagnosticCheckService : ICompilationDiagnosticCheckService
    {
        private readonly List<string> _warningAsErrors = new()
        {
            { "no return statement in function returning non-void" },
        };

        public List<CompilationDiagnostic> MarkWarningsAsErrors(List<CompilationDiagnostic> diagnostics)
        {
            diagnostics.ForEach(d => d.WarningAsError = _warningAsErrors.Any(e => d.Message.Contains(e, StringComparison.OrdinalIgnoreCase)));

            return diagnostics;
        }
    }
}
