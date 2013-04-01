using System;
using System.Collections.Specialized;
using System.Configuration;
using CommandLine;

namespace TCAutoBackup
{
    internal class Program
    {
        private static int Main(string[] args)
        {
            // see https://github.com/gsscoder/commandline
            BackupCommandLineOptions options = new BackupCommandLineOptions();

            if (args != null && args.Length > 0)
            {
                if (!Parser.Default.ParseArguments(args, options))
                {
                    return 1;
                }
            }
            else
            {
                NameValueCollection settings = ConfigurationManager.AppSettings;
                options.TeamCityServerUrl = settings["TeamCityServerUrl"];
                options.FileName = settings["FileName"];
                options.AddTimestamp = Convert.ToBoolean(settings["AddTimestamp"]);
                options.IncludeConfigs = Convert.ToBoolean(settings["IncludeConfigs"]);
                options.IncludeDatabase = Convert.ToBoolean(settings["IncludeDatabase"]);
                options.IncludeBuildLogs = Convert.ToBoolean(settings["IncludeBuildLogs"]);
                options.IncludePersonalChanges = Convert.ToBoolean(settings["IncludePersonalChanges"]);
                options.AuthUserName = settings["AuthUserName"];
                options.AuthPassword = settings["AuthPassword"];
                options.BackupPath = settings["BackupPath"];
                options.NumberOfDaysToKeepBackups = Convert.ToInt32(settings["NumberOfDaysToKeepBackups"]);
            }

            try
            {
                BackupRunner runner = new BackupRunner(options, new FileSystem());
                runner.Cleanup();
                runner.Backup();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return 1;
            }

            return 0;
        }
    }
}