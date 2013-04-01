using System;
using Moq;
using NUnit.Framework;

namespace TCAutoBackup.Tests
{
    [TestFixture]
    public class BackupRunnerFixture
    {
        [Test]
        public void Cleanup()
        {
            BackupCommandLineOptions options = new BackupCommandLineOptions
                                               {BackupPath = "backup", NumberOfDaysToKeepBackups = 30};

            Mock<IFileSystem> mockFileSystem = new Mock<IFileSystem>();
            mockFileSystem.Setup(fs => fs.DirectoryExists(options.BackupPath))
                .Returns(true)
                .Verifiable();

            Mock<IFileSystemInfo> mockTodayFile = new Mock<IFileSystemInfo>();
            mockTodayFile.SetupGet(file => file.LastWriteTime)
                .Returns(DateTime.Today)
                .Verifiable();
            mockTodayFile.SetupGet(file => file.FullName)
                .Returns(@"c:\path\to\file\today.zip")
                .Verifiable();

            Mock<IFileSystemInfo> mockLastMonthFile = new Mock<IFileSystemInfo>();
            mockLastMonthFile.SetupGet(file => file.LastWriteTime)
                .Returns(DateTime.Today.AddDays(-35))
                .Verifiable();
            mockLastMonthFile.SetupGet(file => file.FullName)
                .Returns(@"c:\path\to\file\lastMonth.zip")
                .Verifiable();

            mockFileSystem.Setup(fs => fs.GetFiles(options.BackupPath))
                .Returns(new[] {mockTodayFile.Object, mockLastMonthFile.Object})
                .Verifiable();
            mockFileSystem.Setup(fs => fs.DeleteFile(mockLastMonthFile.Object.FullName))
                .Verifiable();


            BackupRunner runner = new BackupRunner(options, mockFileSystem.Object);
            runner.Cleanup();

            mockFileSystem.Verify(fs => fs.DeleteFile(mockTodayFile.Object.FullName), Times.Never());
            mockFileSystem.Verify();
            mockTodayFile.Verify();
            mockLastMonthFile.Verify();
        }
    }
}