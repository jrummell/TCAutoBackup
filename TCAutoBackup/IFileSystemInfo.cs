using System;

namespace TCAutoBackup
{
    public interface IFileSystemInfo
    {
        DateTime CreationTime { get; }
        string FullName { get; }
    }
}