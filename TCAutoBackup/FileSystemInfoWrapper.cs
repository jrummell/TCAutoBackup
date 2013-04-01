using System;
using System.IO;

namespace TCAutoBackup
{
    public class FileSystemInfoWrapper : IFileSystemInfo
    {
        private readonly FileSystemInfo _fileSystemInfo;

        public FileSystemInfoWrapper(FileSystemInfo fileSystemInfo)
        {
            if (fileSystemInfo == null)
            {
                throw new ArgumentNullException("fileSystemInfo");
            }
            _fileSystemInfo = fileSystemInfo;
        }

        #region IFileSystemInfo Members

        public DateTime LastWriteTime
        {
            get { return _fileSystemInfo.LastWriteTime; }
        }

        public string FullName
        {
            get { return _fileSystemInfo.FullName; }
        }

        #endregion
    }
}