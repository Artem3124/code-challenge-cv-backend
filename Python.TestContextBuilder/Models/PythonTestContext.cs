namespace Python.TestContextBuilder.Models
{
    public class PythonTestContext
    {
        public List<PythonFile> Files { get; set; }

        public string EntryPointFileName { get; set; }

        public PythonTestContext(List<PythonFile> files, string entryPointFileName)
        {
            Files = files;
            EntryPointFileName = entryPointFileName;
        }
    }
}