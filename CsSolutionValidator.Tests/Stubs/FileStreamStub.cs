using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolutionValidator.Cs.Tests.Stubs
{
    internal class FileStreamStub : FileStream
    {
        public string _path { get; set; }
        public FileMode _mode { get; set; }
        public FileAccess _access { get; set; }
        public FileStreamStub(string path, FileMode mode, FileAccess access) : base(path, mode, access)
        {
            _path = path;
            _mode = mode;
            _access = access;
        }
    }
}
