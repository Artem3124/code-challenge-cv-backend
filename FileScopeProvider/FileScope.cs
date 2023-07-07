using FileScopeProvider.Interfaces;
using FileScopeProvider.Models;

namespace FileScopeProvider
{
    internal class FileScope : IFileScope
    {
        private readonly IFileService _fileService;
        private readonly IFilePurger _filePurger;
        private readonly IEnumerable<SystemFile> _files;
        private bool _disposed;

        public FileScope(IFileService fileService, IFilePurger filePurger, List<SystemFile> files)
        {
            _fileService = fileService;
            _filePurger = filePurger;
            _files = files ?? new List<SystemFile>();

            CreateFiles();
        }

        public void Dispose()
        {
            if (_disposed)
            {
                return;
            }

            DeleteFiles();

            _disposed = true;
            GC.SuppressFinalize(this);
        }

        private void CreateFiles()
        {
            foreach (var file in _files)
            {
                _fileService.Write(file.Content, file.Name);
            }
        }

        private void DeleteFiles()
        {
            foreach (var file in _files)
            {
                _filePurger.Delete(file.Name);
            }
        }
    }
}