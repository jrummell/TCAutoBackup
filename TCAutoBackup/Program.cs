using System;
using System.Collections.Specialized;
using System.Configuration;
using System.IO;
using System.Net;
using System.Net.Http;
using CommandLine;

namespace TCAutoBackup
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            BackupCommandLineOptions options = new BackupCommandLineOptions();

            if (args != null && args.Length > 0)
            {
                ICommandLineParser commandLineParser = new CommandLineParser();
                commandLineParser.ParseArguments(args, options);
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

            CleanupBackupDirectory(options.BackupPath, options.NumberOfDaysToKeepBackups);

            SendBackupRequest(options);
        }

        private static void SendBackupRequest(BackupCommandLineOptions options)
        {
            // make post request to initiate backup
            string teamCityServerUrl = options.TeamCityServerUrl;

            // append '/' to base url if it's not already there
            if (!teamCityServerUrl.EndsWith("/"))
            {
                teamCityServerUrl += "/";
            }

            // make request
            string requestUri =
                string.Format(
                    "/httpAuth/app/rest/server/backup?includeConfigs={0}&includeDatabase={1}&includeBuildLogs={2}&fileName={3}&addTimestamp={4}&includePersonalChanges={5}",
                    options.IncludeConfigs,
                    options.IncludeDatabase,
                    options.IncludeBuildLogs,
                    options.FileName,
                    options.AddTimestamp,
                    options.IncludePersonalChanges);

            var handler = new HttpClientHandler
                          {
                              Credentials = new NetworkCredential
                                            {
                                                UserName = options.AuthUserName,
                                                Password = options.AuthPassword
                                            }
                          };
            var client = new HttpClient(handler)
                         {
                             BaseAddress = new Uri(teamCityServerUrl)
                         };

            var result = client.PostAsync(requestUri, null).Result;

            if (result.StatusCode == HttpStatusCode.OK)
            {
                Console.WriteLine("Returned status-code was 200 - OK. Backup successful");
            }
            else
            {
                Console.WriteLine(result);
                Environment.ExitCode = 1;
            }
        }

        private static void CleanupBackupDirectory(string backupPath, int numberOfDaysToKeepBackups)
        {
            Console.WriteLine("Cleanup backup directory");

            // delete files older than a number of days days
            if (!Directory.Exists(backupPath))
            {
                return;
            }
            var files = Directory.GetFiles(backupPath);
            foreach (string file in files)
            {
                var fi = new FileInfo(file);
                if (fi.LastAccessTime < DateTime.Now.AddDays(-numberOfDaysToKeepBackups))
                {
                    Console.WriteLine("Deleting file {0}", file);
                    fi.Delete();
                }
            }
        }
    }
}