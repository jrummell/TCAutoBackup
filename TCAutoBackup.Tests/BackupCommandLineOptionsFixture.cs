using CommandLine;
using NUnit.Framework;

namespace TCAutoBackup.Tests
{
    [TestFixture]
    public class BackupCommandLineOptionsFixture
    {
        [Test]
        public void DisplayOptions()
        {
            string[] args = new string[0];
            BackupCommandLineOptions options = new BackupCommandLineOptions();
            Parser.Default.ParseArguments(args, options);
        }
    }
}