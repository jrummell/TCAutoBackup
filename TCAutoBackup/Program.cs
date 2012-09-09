using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace TCAutoBackup
{
    class Program
    {
        static void Main(string[] args)
        {
            CleanupBackupDirectory();

            SendBackupRequest();
        }

        private static void SendBackupRequest()
        {
            // make post request to initate backup
            var teamcityServerUrl = ConfigurationManager.AppSettings["TeamCityServerUrl"];
            var filename = ConfigurationManager.AppSettings["FileName"];
            var addTimestamp = Convert.ToBoolean(ConfigurationManager.AppSettings["AddTimestamp"]);
            var includeConfigs = Convert.ToBoolean(ConfigurationManager.AppSettings["IncludeConfigs"]);
            var includeDatabase = Convert.ToBoolean(ConfigurationManager.AppSettings["IncludeDatabase"]);
            var includeBuildLogs = Convert.ToBoolean(ConfigurationManager.AppSettings["IncludeBuildLogs"]);
            var includePersonalChanges = Convert.ToBoolean(ConfigurationManager.AppSettings["IncludePersonalChanges"]);

            // append '/' to base url if it's not already there
            if (!teamcityServerUrl.EndsWith("/"))
            {
                teamcityServerUrl += "/";
            }

            // make request
            var requestUri =
                "/httpAuth/app/rest/server/backup" +
                "?includeConfigs=" + includeConfigs +
                "&includeDatabase={2}" + includeDatabase +
                "&includeBuildLogs={3}" + includeBuildLogs +
                "&fileName=" + filename +
                "&addTimestamp" + addTimestamp +
                "&includePersonalChanges" + includePersonalChanges;

            var handler = new HttpClientHandler
            {
                Credentials = new NetworkCredential
                {
                    UserName = ConfigurationManager.AppSettings["AuthUserName"],
                    Password = ConfigurationManager.AppSettings["AuthPassword"]
                }
            };
            var client = new HttpClient(handler)
            {
                BaseAddress = new Uri(teamcityServerUrl)
            };
            
            var result = client.PostAsync(requestUri, null).Result;

            if (result.StatusCode == HttpStatusCode.OK)
            {
                Console.WriteLine("Returned status-code was 200 - OK. Backup succcessful");
            }
            else
            {
                Console.WriteLine(result);
                Environment.ExitCode = 1;
            }
        }

        private static void CleanupBackupDirectory()
        {
            Console.WriteLine("Cleanup backup directory");
            var backupPath = ConfigurationManager.AppSettings["BackupPath"];
            var numberOfDaysToKeepBackups = Convert.ToInt32(ConfigurationManager.AppSettings["NumberOfDaysToKeepBackups"]);

            // delete files older than a number of days days
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
