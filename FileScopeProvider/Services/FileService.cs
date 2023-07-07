using FileScopeProvider.Interfaces;

namespace FileScopeProvider.Services
{
    public class FileService : IFileService
    {
        public void Write(string content, string fileName)
        {
            using var file = new StreamWriter(fileName);

            file.WriteLine(content);

            file.Close();
        }

        public string Read(string fileName)
        {
            using var fileStream = new StreamReader(fileName);

            var content = fileStream.ReadToEnd();
            fileStream.Close();

            return content;
        }
    }
}
