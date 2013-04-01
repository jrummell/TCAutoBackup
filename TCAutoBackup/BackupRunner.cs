using System;
using System.Linq;
using System.Net;
using System.Net.Http;

namespace TCAutoBackup
{
    public class BackupRunner
    {
        private readonly IFileSystem _fileSystem;
        private readonly BackupCommandLineOptions _options;

        public BackupRunner(BackupCommandLineOptions options, IFileSystem fileSystem)
        {
            if (options == null)
            {
                throw new ArgumentNullException("options");
            }
            _options = options;
            _fileSystem = fileSystem;
        }

        public void Backup()
        {
            // make post request to initiate backup
            string teamCityServerUrl = _options.TeamCityServerUrl;

            // append '/' to base url if it's not already there
            if (!teamCityServerUrl.EndsWith("/"))
            {
                teamCityServerUrl += "/";
            }

            // make request
            string requestUri =
                string.Format(
                    "/httpAuth/app/rest/server/backup?includeConfigs={0}&includeDatabase={1}&includeBuildLogs={2}&fileName={3}&addTimestamp={4}&includePersonalChanges={5}",
                    _options.IncludeConfigs,
                    _options.IncludeDatabase,
                    _options.IncludeBuildLogs,
                    _options.FileName,
                    _options.AddTimestamp,
                    _options.IncludePersonalChanges);

            var handler = new HttpClientHandler
                          {
                              Credentials = new NetworkCredential
                                            {
                                                UserName = _options.AuthUserName,
                                                Password = _options.AuthPassword
                                            }
                          };
            var client = new HttpClient(handler)
                         {
                             BaseAddress = new Uri(teamCityServerUrl, UriKind.Absolute)
                         };

            var result = client.PostAsync(new Uri(requestUri, UriKind.Relative), null).Result;

            if (result.StatusCode == HttpStatusCode.OK)
            {
                Console.WriteLine("Returned status-code was 200 - OK. Backup successful");
            }
            else
            {
                throw new Exception(result.ToString());
            }
        }

        public void Cleanup()
        {
            Console.WriteLine("Cleaning up backup directory ...");

            // delete files older than a number of days days
            if (!_fileSystem.DirectoryExists(_options.BackupPath))
            {
                return;
            }
            var files = _fileSystem.GetFiles(_options.BackupPath)
                .Where(file => file.LastWriteTime < DateTime.Now.AddDays(-_options.NumberOfDaysToKeepBackups));

            foreach (var file in files)
            {
                Console.WriteLine("Deleting file {0}", file.FullName);
                _fileSystem.DeleteFile(file.FullName);
            }
        }
    }
}