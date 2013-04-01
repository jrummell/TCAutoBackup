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

        public DateTime CreationTime
        {
            get { return _fileSystemInfo.CreationTime; }
        }

        public string FullName
        {
            get { return _fileSystemInfo.FullName; }
        }

        #endregion
    }
}