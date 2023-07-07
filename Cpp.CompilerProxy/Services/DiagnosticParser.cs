using Shared.Core.Compilers;

namespace Cpp.CompilerProxy.Services
{
    internal class DiagnosticParser : IDiagnosticParser
    {
        public List<CompilationDiagnostic> StringToDiagnostic(string value)
        {
            var result = new List<CompilationDiagnostic>();

            var lines = value.Split('\n');

            foreach (var line in lines)
            {
                result.Add(StringToDiagnosticSingle(line));
            }
            return result;
        }

        private CompilationDiagnostic StringToDiagnosticSingle(string value)
        {
            var diagnostic = new CompilationDiagnostic();
            var qoutes = value.Split(':');

            if (qoutes.Length > 1 && int.TryParse(qoutes[1], out var dLine))
            {
                diagnostic.Line = dLine;
            }
            if (qoutes.Length > 2 && int.TryParse(qoutes[2], out var column))
            {
                diagnostic.Column = column;
            }
            if (qoutes.Length > 3)
            {
                diagnostic.Severity = ParseSeverity(qoutes[3]);
            }
            if (qoutes.Length > 4)
            {
                diagnostic.Message = string.Join(':', qoutes.Skip(4));
            }

            return diagnostic;
        }

        private DiagnosticSeverityInternal ParseSeverity(string value)
        {
            value = value.Trim();
            if (value == "error")
            {
                return DiagnosticSeverityInternal.Error;
            }
            if (value == "warning")
            {
                return DiagnosticSeverityInternal.Warning;
            }
            if (value == "note")
            {
                return DiagnosticSeverityInternal.Note;
            }

            return DiagnosticSeverityInternal.Unknown;
        }

    }
}
