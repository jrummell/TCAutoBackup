using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace TCAutoBackup
{
    public class FileSystem : IFileSystem
    {
        public bool DirectoryExists(string path)
        {
            return Directory.Exists(path);
        }

        public IEnumerable<IFileSystemInfo> GetFiles(string path)
        {
            return new DirectoryInfo(path)
                .GetFileSystemInfos()
                .Select(file => new FileSystemInfoWrapper(file));
        }

        public void DeleteFile(string path)
        {
            File.Delete(path);
        }
    }
}