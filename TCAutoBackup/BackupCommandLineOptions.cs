using CommandLine;
using CommandLine.Text;

namespace TCAutoBackup
{
    public class BackupCommandLineOptions
    {
        [Option('s', "teamCityServerUrl", DefaultValue = "http://localhost:81/", HelpText = "TeamCity Server URL",
            Required = true)]
        public string TeamCityServerUrl { get; set; }

        [Option('u', "authUserName", Required = true)]
        public string AuthUserName { get; set; }

        [Option('p', "authPassword", Required = true)]
        public string AuthPassword { get; set; }

        [Option('b', "backupPath", Required = true)]
        public string BackupPath { get; set; }

        [Option('h', "numberOfDaysToKeepBackups", DefaultValue = 30)]
        public int NumberOfDaysToKeepBackups { get; set; }

        [Option('f', "filename")]
        public string FileName { get; set; }

        [Option('t', "addTimestamp", DefaultValue = true)]
        public bool AddTimestamp { get; set; }

        [Option('c', "includeConfig", DefaultValue = true)]
        public bool IncludeConfigs { get; set; }

        [Option('d', "includeDatabase", DefaultValue = true)]
        public bool IncludeDatabase { get; set; }

        [Option('l', "includeBuildLogs", DefaultValue = true)]
        public bool IncludeBuildLogs { get; set; }

        [Option('g', "includePersonalChanges", DefaultValue = true)]
        public bool IncludePersonalChanges { get; set; }

        [HelpOption('?', "help")]
        public string GetUsage()
        {
            return HelpText.AutoBuild(this, current => HelpText.DefaultParsingErrorsHandler(this, current));
        }
    }
}