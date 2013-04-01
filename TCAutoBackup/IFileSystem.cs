using System.Collections.Generic;

namespace TCAutoBackup
{
    public interface IFileSystem
    {
        bool DirectoryExists(string path);
        IEnumerable<IFileSystemInfo> GetFiles(string path);
        void DeleteFile(string path);
    }
}