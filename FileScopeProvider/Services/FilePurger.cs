using FileScopeProvider.Interfaces;

namespace FileScopeProvider.Services
{
    internal class FilePurger : IFilePurger
    {
        public bool Delete(string path)
        {
            var fileExists = File.Exists(path);

            if (fileExists)
            {
                File.Delete(path);
            }

            return fileExists;
        }
    }
}
