using System;

namespace TCAutoBackup
{
    public interface IFileSystemInfo
    {
        DateTime LastWriteTime { get; }
        string FullName { get; }
    }
}