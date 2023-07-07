namespace Shared.Core.Compilers
{
    public class CompilationDiagnostic
    {
        public string Message { get; set; } = string.Empty;

        public DiagnosticSeverityInternal Severity { get; set; }

        public bool WarningAsError { get; set; }

        public int Column { get; set; } = -1;

        public int Line { get; set; } = -1;

        public CompilationDiagnostic()
        {

        }

        public CompilationDiagnostic(string message, DiagnosticSeverityInternal severity, int column, int line)
        {
            Message = message;
            Severity = severity;
            Column = column;
            Line = line;
        }
    }
}
