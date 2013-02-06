using CommandLine;
using CommandLine.Text;

namespace TCAutoBackup
{
    public class BackupCommandLineOptions : CommandLineOptionsBase
    {
        [Option("teamCityServerUrl", "server", DefaultValue = "http://localhost:81/", HelpText = "TeamCity Server URL", Required = true)]
        public string TeamCityServerUrl { get; set; }

        [Option("authUserName", "username", Required = true)]
        public string AuthUserName { get; set; }

        [Option("authPassword", "password", Required = true)]
        public string AuthPassword { get; set; }

        [Option("backupPath", "path", Required = true)]
        public string BackupPath { get; set; }

        [Option("numberOfDaysToKeepBackups", "backupDays", DefaultValue = 7)]
        public int NumberOfDaysToKeepBackups { get; set; }

        [Option("file", "filename")]
        public string FileName { get; set; }

        [Option("addTimestamp", "timestamp", DefaultValue = true)]
        public bool AddTimestamp { get; set; }

        [Option("includeConfig", "config", DefaultValue = true)]
        public bool IncludeConfigs { get; set; }

        [Option("includeDatabase", "db", DefaultValue = true)]
        public bool IncludeDatabase { get; set; }

        [Option("includeBuildLogs", "logs", DefaultValue = true)]
        public bool IncludeBuildLogs { get; set; }

        [Option("includePersonalChanges", "personal", DefaultValue = true)]
        public bool IncludePersonalChanges { get; set; }

        [HelpOption]
        public string GetUsage()
        {
            return HelpText.AutoBuild(this, current => HelpText.DefaultParsingErrorsHandler(this, current));
        }
    }
}